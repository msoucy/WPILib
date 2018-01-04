using HAL.Base;
using static HAL.Base.HALDriverStation;
using static HAL.Base.HAL;

namespace WPILib
{
    /// <summary>
    /// IterativeRobot implements the IterativeRobotBase robot program framework.
    /// <remarks>
    /// The IterativeRobot class is intended to be subclassed by a user creating a robot program.
    /// <para/>
    /// periodic() functions from the base class are called each time a new packet is received from
    /// the driver station.
    /// </remarks>
    public class IterativeRobot : IterativeRobotBase
    {
        public IterativeRobot()
        {
            Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);
        }

        /// <summary>
        /// Provide an alternate "main loop" via startCompetition().
        /// </summary>
        public override void StartCompetition()
        {
            RobotInit();

            // Tell the DS that the robot is ready to be enabled.
            HAL_ObserveUserProgramStarting();
            
            while (true)
            {
                // Wait for new data to arrive
                m_ds.WaitForData();
                LoopFunc();
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
