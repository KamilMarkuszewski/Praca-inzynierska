using System;
using System.Collections.Generic;
using System.Text;

namespace LocalPlayerData
{
    public class MyChars
    {
        public bool nochars;
        public int actual;
        public int count;
        public MyChar[] postacie;

        public MyChars() {
            postacie = new MyChar[10];
            nochars = false;
            count = 0;
        }

    }
}
