using System;
using System.Threading;

namespace Counter
{

    public delegate void MethodCounterN();
    public delegate void MethodCounterStart();
    public delegate string MethodCounterStart1();
    public delegate void MethodCounter();
    interface ICutDownNotifier
    {
        void Init();
        void Run();
    }
    class Program
    {

        static void Main(string[] args)
        {
            Thread myThread1;
            Thread myThread2 ;
            Thread myThread3 ;
            Console.WriteLine("Sec:");
            int sec = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Sec 2:");
            int sec2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Name 2:");
            string name2 = Console.ReadLine();
            Console.WriteLine("Sec 3:");
            int sec3 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Name 3:");
            string name3 = Console.ReadLine();
            MetodCounterRealiz sub = new MetodCounterRealiz(sec, name);
            MetodCounterRealiz sub2 = new MetodCounterRealiz(sec2, name2);
            MetodCounterRealiz sub3 = new MetodCounterRealiz(sec3, name3);
            AnonMetodCounterRealiz sub4 = new AnonMetodCounterRealiz(sec, name);
            AnonMetodCounterRealiz sub5 = new AnonMetodCounterRealiz(sec2, name2);
            AnonMetodCounterRealiz sub6 = new AnonMetodCounterRealiz(sec3, name3);
            LambdaMetodCounterRealiz sub7 = new LambdaMetodCounterRealiz(sec, name,1);
            LambdaMetodCounterRealiz sub8 = new LambdaMetodCounterRealiz(sec2, name2,1);
            LambdaMetodCounterRealiz sub9 = new LambdaMetodCounterRealiz(sec3, name3,0);
            Console.Clear();
            Console.WriteLine("Запустить обработку при помощи:\n 1-Методы\n 2-Анонимные делегаты \n 3-Лямбда выражения");
            int key = Convert.ToInt32(Console.ReadLine());
            if (key == 1) {
                 myThread1 = new Thread(new ThreadStart(sub.Run));
                 myThread2 = new Thread(new ThreadStart(sub2.Run));
                 myThread3 = new Thread(new ThreadStart(sub3.Run));
                myThread1.Start();
                myThread2.Start();
                myThread3.Start();
            }
            else if (key == 2) {
                 myThread1 = new Thread(new ThreadStart(sub4.Run));
                 myThread2 = new Thread(new ThreadStart(sub5.Run));
                 myThread3 = new Thread(new ThreadStart(sub6.Run));
                myThread1.Start();
                myThread2.Start();
                myThread3.Start();
            }
            else if (key == 3) {
                myThread1 = new Thread(new ThreadStart(sub7.Run));
                myThread2 = new Thread(new ThreadStart(sub8.Run));
                myThread3 = new Thread(new ThreadStart(sub9.Run));
                myThread1.Start();
                myThread2.Start();
                myThread3.Start();
            }
            else { 
                Console.Clear();
                Console.WriteLine("Incorrect data");
                Console.ReadLine();
            }
        }

    }
    class Counter
    {
        public int seconds = 5;

        public event MethodCounter onCount;

        public event MethodCounter StartCount;

        public event MethodCounterN NCount;
        public Counter(int seconds)
        {
            this.seconds = seconds;
        }


        public void SecondsCounter()
        {
            StartCount();
            for (int i = 0; i <= seconds; seconds--)
            {
                Thread.Sleep(1000);
                NCount();

                if (i == seconds)
                {
                    onCount();
                }

            }
        }
    }

    class MetodCounterRealiz : ICutDownNotifier
    {


        int second;
        string name;
        Counter count;
        public MetodCounterRealiz(int sec, string name)
        {
            this.name = name;
            this.second = sec;
            count = new Counter(sec);
        }
        
        public void Init()
        {

            count.onCount += SecondsAlert;
            count.StartCount += StartCount;
            count.NCount += NSec;


        }
        public void Run()
        {
            Init();
            count.SecondsCounter();

        }
        public void SecondsAlert()
        {

            Console.WriteLine(name + " Закончил отсчет!");

        }
        public void StartCount()
        {

            Console.WriteLine(name + " Начал отсчет!");
            Console.WriteLine("Время ожидаения: " + second);


        }
        public void NSec()
        {
            Console.WriteLine("Таймеру: " + name + " осталось: " + Convert.ToString(count.seconds));
        }
    }

    class AnonMetodCounterRealiz : ICutDownNotifier
    {
        

        int second;
        string name;
        Counter count;
        public AnonMetodCounterRealiz(int sec, string name)
        {
            this.name = name;
            this.second = sec;
            count = new Counter(sec);

        }

       


        public void Init()
        {
            count.onCount += end();
            count.StartCount += Start() ;
            count.NCount += Ncount() ;
        }
        public  MethodCounter  Start()
        {
            MethodCounter del = delegate ()
            {
                string str = name + " Начал отсчет!";
                Console.WriteLine(str);
            };
            return del;
        }
        public MethodCounter end()
        {
            MethodCounter del = delegate ()
            {
                string str = name + " Закончил отсчет!";
                Console.WriteLine(str);
            };
            return del;
        }
        public MethodCounterN Ncount()
        {
            MethodCounterN del = delegate ()
            {
                Console.WriteLine("Таймеру: " + name + " осталось: " + Convert.ToString(count.seconds));
            };
            return del;
        }

        public void Run()
        {
            Init();
            count.SecondsCounter();
        }
       
    }

    class LambdaMetodCounterRealiz : ICutDownNotifier
    {


        int second, test;
        string name;
        Counter count;
        public LambdaMetodCounterRealiz(int sec, string name,int test)
        {
            this.name = name;
            this.second = sec;
            this.test = test;
            count = new Counter(sec);

        }




        public void Init()
        {
            count.onCount += end();
            count.StartCount += Start();
            count.NCount += Ncount();
        }
        public MethodCounter Start()
        {
            MethodCounter del =  () =>
            {
                string str = name + " Начал отсчет!";
                Console.WriteLine(str);
            };
            return del;
        }
        public MethodCounter end()
        {
            MethodCounter del =  () =>
            {
                string str = name + " Закончил отсчет!";
                Console.WriteLine(str);
            };
            return del;
        }
        public MethodCounterN Ncount()
        {
            MethodCounterN del =  () =>
            {

                string str = "Таймеру: " + name + " осталось: " + Convert.ToString(count.seconds)+"\n";
                if (test != 0) { str = ""; }
                Console.Write(str);
            };
            return del;
        }

        public void Run()
        {
            Init();
            count.SecondsCounter();
        }

    }
}
