namespace DiningPhilosophers;

public class Philosopher
{
    public int EatCount;
    public bool IsEating = false;
    private Thread _philosopherThread;
    public Semaphore[] Forks = new Semaphore[2];
    public Semaphore Table;
    private bool _dinner = true;
    

    public Philosopher(Semaphore table, params Semaphore[] forks)
    {
        EatCount = 0;
        Table = table;
        Forks = forks;
        _philosopherThread = new Thread(PhilosopherMethod);
    }

    public void StartDinner()
    {
        _philosopherThread.Start();
    }

    public void WaitEnd()
    {
        _philosopherThread.Join();
    }

    public void StopDinner()
    {
        _dinner = false;
    }

    public void PhilosopherMethod()
    {
        while (_dinner)
        {
            Think();
            if (EatCount != 0) continue;
            Table.WaitOne();
            Forks[0].WaitOne();
            Forks[1].WaitOne();
            Eat();
            EatCount++;
            Forks[0].Release();
            Forks[1].Release();
            Table.Release();
            IsEating = false;
        }
    }

    private void Think()
    {
        Thread.Sleep(300);
    }

    private void Eat()
    {
        IsEating = true;
        Thread.Sleep(600);
    }
}