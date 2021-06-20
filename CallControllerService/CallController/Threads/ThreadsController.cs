using System.Threading;
using CallController;
using CallControllerService.CallController.Ticks;

namespace CallControllerService.CallController.Threads
{
    public class ThreadsController
    {
        //Define Tick Controller instance
        private readonly TicksController _ticksController;

        //Define threads
        private readonly Thread _updateTicksThread;

        public ThreadsController()
        {
            //Get instances
            _ticksController = InstancesContainer._TicksController;

            //Create threads instances
            _updateTicksThread = new Thread(() => _ticksController?.UpdateTicks());

            //Start threads
            StartThreads();
        }

        private void StartThreads()
        {
            _updateTicksThread.Start();
        }

        private void StopThreads()
        {
            _updateTicksThread.Abort();
        }

        private void OnApplicationExit()
        {
            StopThreads();
        }
    }
}
