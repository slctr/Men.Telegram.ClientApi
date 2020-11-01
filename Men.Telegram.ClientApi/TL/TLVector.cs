using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleSharp.TL
{
    public class TLVector<T> : TLObject, IList<T>
    {
        [TLObject(481674261)]
        private List<T> lists;

        public TLVector(IEnumerable<T> collection)
        {
            this.lists = new List<T>(collection);
        }

        public TLVector()
        {
            this.lists = new List<T>();
        }

        public T this[int index]
        {
            get { return this.lists[index]; }
            set { this.lists[index] = value; }
        }

        public override int Constructor
        {
            get
            {
                return 481674261;
            }
        }

        public int Count => this.lists.Count;

        public bool IsReadOnly => ((IList<T>)this.lists).IsReadOnly;

        public void Add(T item)
        {
            this.lists.Add(item);
        }

        public void Clear()
        {
            this.lists.Clear();
        }

        public bool Contains(T item)
        {
            return this.lists.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.lists.CopyTo(array, arrayIndex);
        }

        public override void DeserializeBody(BinaryReader br)
        {
            int count = br.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                if (typeof(T) == typeof(int))
                {
                    this.lists.Add((T)Convert.ChangeType(br.ReadInt32(), typeof(T)));
                }
                else if (typeof(T) == typeof(long))
                {
                    this.lists.Add((T)Convert.ChangeType(br.ReadInt64(), typeof(T)));
                }
                else if (typeof(T) == typeof(string))
                {
                    this.lists.Add((T)Convert.ChangeType(StringUtil.Deserialize(br), typeof(T)));
                }
                else if (typeof(T) == typeof(double))
                {
                    this.lists.Add((T)Convert.ChangeType(br.ReadDouble(), typeof(T)));
                }
                else if (typeof(T).BaseType == typeof(TLObject))
                {
                    int constructor = br.ReadInt32();
                    Type type = TLContext.getType(constructor);
                    object obj = Activator.CreateInstance(type);
                    type.GetMethod("DeserializeBody").Invoke(obj, new object[] { br });
                    this.lists.Add((T)Convert.ChangeType(obj, type));
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.lists.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this.lists.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.lists.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return this.lists.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.lists.RemoveAt(index);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(this.Constructor);
            bw.Write(this.lists.Count());

            foreach (T item in this.lists)
            {
                if (typeof(T) == typeof(int))
                {
                    int res = (int)Convert.ChangeType(item, typeof(int));

                    bw.Write(res);
                }
                else if (typeof(T) == typeof(long))
                {
                    long res = (long)Convert.ChangeType(item, typeof(long));
                    bw.Write(res);
                }
                else if (typeof(T) == typeof(string))
                {
                    string res = (string)(Convert.ChangeType(item, typeof(string)));
                    StringUtil.Serialize(res, bw);
                }
                else if (typeof(T) == typeof(double))
                {
                    double res = (double)Convert.ChangeType(item, typeof(double));
                    bw.Write(res);
                }
                else if (typeof(T).BaseType == typeof(TLObject))
                {
                    TLObject res = (TLObject)(object)item;
                    res.SerializeBody(bw);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.lists.GetEnumerator();
        }
    }
}
