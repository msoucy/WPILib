using HAL.Base;
using static HAL.Base.HALDriverStation;
using static HAL.Base.HAL;
using System.Threading;

namespace WPILib
{
    /// <summary>
    /// TimedRobot implements the IterativeRobotBase robot program framework.
    /// <remarks>
    /// The TimedRobot class is intended to be subclassed by a user creating a robot program.
    /// <para/>
    /// periodic() functions from the base class are called on an interval by a Notifier instance.
    /// </remarks>
    public class TimedRobot : IterativeRobotBase
    {
        public static double DEFAULT_PERIOD = 0.02;

        private double m_period = DEFAULT_PERIOD;

        // Prevents loop from starting if user calls setPeriod() in robotInit()
        private bool m_startLoop = false;

        private Notifier m_loop;

        public TimedRobot()
        {
            Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);
            m_loop = new Notifier(LoopFunc);
        }

        /// <summary>
        /// Provide an alternate "main loop" via startCompetition().
        /// </summary>
        public override void StartCompetition()
        {
            RobotInit();

            // Tell the DS that the robot is ready to be enabled.
            HAL_ObserveUserProgramStarting();
            
            m_startLoop = true;
            m_loop.StartPeriodic(m_period);
            while (true)
            {
                try
                {
                    Thread.CurrentThread.Join(1000 * 60 * 60 * 24);
                }
                catch (ThreadInterruptedException)
                {
                    Thread.CurrentThread.Interrupt();
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }



        /// <summary>
        /// Set time period between calls to Periodic() functions.
        /// </summary>
        /// <param name="period">Period in seconds</param>
        public void SetPeriod(double period)
        {
            m_period = period;

            if (m_startLoop)
            {
                m_loop.StartPeriodic(m_period);
            }
        }
    }
}
