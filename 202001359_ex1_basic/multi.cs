using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _202001359_ex1_basic
{

    class multi
    {
        Thread t1, t2;
        AxWindowsMediaPlayer s1, s2;
        Random rnd = new Random();
        List<string> rand;
        internal void Start(AxWMPLib.AxWindowsMediaPlayer wmp, AxWindowsMediaPlayer wmp2, List<string> url)
        {
            s1 = wmp;
            s2 = wmp2;
            rand = url;
            t1 = new Thread(new ThreadStart(first));
            t1.Start();
            
            t2 = new Thread(new ThreadStart(second));
            t2.Start();
          
        }

        private void second()
        {
            for (int i = 0; i < 3; i++)
            {
                s2.URL = rand[rnd.Next(3)];
                s2.Ctlcontrols.play();
                Thread.Sleep(7000);
            }
           
        }

        private void first()
        {
            for (int i = 0; i < 3; i++)
            {
                s1.URL = rand[rnd.Next(3)];
                s1.Ctlcontrols.play();
                Thread.Sleep(5000);
            }
          
        }
    }

}
