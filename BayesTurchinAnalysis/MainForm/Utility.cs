using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BayesTurchinAnalysis
{
    class ControlFactory
    {
        public static T Create<T>(Point location, Size size, string text, string name = null,
            int tabindex = 0) where T : Control, new()
        {
            T control = new T();
            control.AutoSize = true;
            control.Location = location;
            control.Size = size;
            control.TabIndex = 0;
            if (name == null)
            {
                control.Text = control.Name = text;
            }
            else
            {
                control.Name = name;
                control.Text = text;
            }
            return control;
        }
        public static Button CreateButton(Point location, Size size, string text,
            string name, EventHandler clickeventfunc, int tabindex = 0)
        {
            Button button = new Button();
            button.Location = location;
            button.Name = name;
            button.Size = size;
            button.TabIndex = 1;
            button.Text = text;
            button.UseVisualStyleBackColor = true;
            button.Click += clickeventfunc;
            return button;
        }
    }
    class Deque<T> : IEnumerable<T>
    {
        T[] data;           //データ
        int first, last;    //最初の要素番号と最後の要素番号
        int limit;          // 11111111みたいにしておいて＆演算で繰り上がりを削除
        int count;
        public int Count { get { return count; } }
        public int Size { get { return limit + 1; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="capacity_2pown">2^n分だけ確保するのでそのnの値</param>
        public Deque(int capacity_2pown = 4)
        {
            capacity_2pown = pow2(capacity_2pown);
            data = new T[capacity_2pown];
            first = 0;
            last = 0;
            limit = capacity_2pown - 1;
            count = 0;
        }
        public static int pow2(int n)
        {
            if (n < 0)
                return 0;
            return 1 << n;
        }
        public T this[int i]
        {
            get { return this.data[(first + i) & limit]; }
            set { this.data[(first + i) & limit] = value; }
        }
        public bool push_front(T element)
        {
            if (count == data.Length - 1)
                return false;

            first = (first - 1) & limit;
            data[first] = element;
            count++;
            return true;
        }
        public T pop_front()
        {
            first = (first + 1) & limit;
            count--;
            return data[(first - 1) & limit];
        }
        public bool push_back(T element)
        {
            if (count == data.Length - 1)
                return false;

            data[last] = element;
            last = (last + 1) & limit;
            count++;
            return true;
        }
        public T pop_back()
        {
            last = (last - 1) & limit;
            count--;
            return data[(last ) & limit];
        }
        public void push_back_queue(T element)
        {
            if (count == data.Length - 1)
            {
                pop_front();
            }

            data[last] = element;
            last = (last + 1) & limit;
            count++;
        }
        public void extend()
        {
            int next = (Size << 1);
            T[] temp = new T[next];
            for (int i = 0; i < data.Length; i++)
            {
                temp[i] = data[i];
            }

            limit = next - 1;
        }
        public void clear()
        {
            data = new T[limit + 1];
            GC.Collect();
            first = 0;
            last = 0;
            count = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            if (first <= last)
            {
                for (int i = first; i < last; i++)
                    yield return data[i];
            }
            else
            {
                for (int i = first; i < data.Length; i++)
                    yield return data[i];
                for (int i = 0; i < last; i++)
                    yield return data[i];
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}