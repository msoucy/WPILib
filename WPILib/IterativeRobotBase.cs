using HAL.Base;
using static HAL.Base.HALDriverStation;
using static HAL.Base.HAL;
using static System.Console;

namespace WPILib
{
    /// <summary>
    ///  Implements a specific type of robot program framework, extending the <see cref="RobotBase"/> class.
    /// </summary>
    /// <remarks>
    ///  The IterativeRobotBase class does not implement startCompetition(), so it should not be used
    /// by teams directly.
    /// <para/>
    /// This class provides the following functions which are called by the main loop,
    /// startCompetition(), at the appropriate times:
    /// <para/>
    /// robotInit() -- provide for initialization at robot power-on
    /// <para/>
    /// init() functions -- each of the following functions is called once when the
    /// appropriate mode is entered:
    ///   - disabledInit()   -- called only when first disabled
    ///   - autonomousInit() -- called each and every time autonomous is entered from
    ///                         another mode
    ///   - teleopInit()     -- called each and every time teleop is entered from
    ///                         another mode
    ///   - testInit()      -- called each and every time test is entered from
    ///                         another mode
    /// <para/>
    /// periodic() functions -- each of these functions is called on an interval:
    ///   - robotPeriodic()
    ///   - disabledPeriodic()
    ///   - autonomousPeriodic()
    ///   - teleopPeriodic()
    ///   - testPeriodic()
    /// </remarks>
    public abstract class IterativeRobotBase : RobotBase
    {
        private enum Mode
        {
            None,
            Disabled,
            Autonomous,
            Teleop,
            Test
        }
        private Mode m_lastMode = Mode.None;

        /// <summary>
        /// Robot-wide initialization code should go here.
        /// </summary>
        /// <remarks>
        /// Users should override this method for default Robot-wide initialiation which will be called
        /// when the robot is first powered on. It will be called exactly one time.
        /// <para></para>
        /// Warning: The Driver Station "Robot Code" light and FMS "Robot Ready" indicators will be off until
        /// <see cref="RobotInit"/> exits. Code in <see cref="RobotInit"/> that waits for enable will cause
        /// the robot to never indicate that the code is ready, causing the robot to be bypassed in a match.
        /// </remarks>
        public virtual void RobotInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(RobotInit)} method... Overload me!");
        }

        /// <summary>
        /// Initialization code for disabled mode should go here.
        /// </summary>
        public virtual void DisabledInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(DisabledInit)} method... Overload me!");
        }

        /// <summary>
        /// Initialization code for test mode should go here.
        /// </summary>
        public virtual void TestInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TestInit)} method... Overload me!");
        }

        /// <summary>
        /// Initialization code for autonomous mode should go here.
        /// </summary>
        public virtual void AutonomousInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(AutonomousInit)} method... Overload me!");
        }

        /// <summary>
        /// Initialization for teleop mode should go here.
        /// </summary>
        public virtual void TeleopInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TeleopInit)} method... Overload me!");
        }

        private bool m_dpFirstRun = true;

        /// <summary>
        /// Periodic code for disabled mode should go here.
        /// </summary>
        public virtual void DisabledPeriodic()
        {
            if (m_dpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(DisabledPeriodic)} method... Overload me!");
                m_dpFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool m_apFirstRun = true;

        /// <summary>
        /// Periodic code for autonomous mode should go here.
        /// </summary>
        public virtual void AutonomousPeriodic()
        {
            if (m_apFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(AutonomousPeriodic)} method... Overload me!");
                m_apFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool m_tpFirstRun = true;

        /// <summary>
        /// Periodic code for teleop mode should go here.
        /// </summary>
        public virtual void TeleopPeriodic()
        {
            if (m_tpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TeleopPeriodic)} method... Overload me!");
                m_tpFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool m_tmpFirstRun = true;

        /// <summary>
        /// Periodic code for test mose should go here.
        /// </summary>
        public virtual void TestPeriodic()
        {
            if (m_tmpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TestPeriodic)} method... Overload me!");
                m_tmpFirstRun = false;
            }
        }

        private bool m_rpFirstFun = true;

        /// <summary>
        /// Periodic code for all modes should go here.
        /// </summary>
        public virtual void RobotPeriodic()
        {
            if (m_rpFirstFun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(RobotPeriodic)} method... Overload me!");
                m_rpFirstFun = false;
            }
            Timer.Delay(0.001);
        }

        protected void LoopFunc()
        {
            // Call the appropriate function depending upon the current robot mode
            if (IsDisabled)
            {
                // call DisabledInit() if we are now just entering disabled mode from
                // either a different mode or from power-on
                if (m_lastMode != Mode.Disabled)
                {
                    LiveWindow.LiveWindow.SetEnabled(false);
                    DisabledInit();
                    m_lastMode = Mode.Disabled;
                }
                HAL_ObserveUserProgramDisabled();
                DisabledPeriodic();
            }
            else if (IsAutonomous)
            {
                // call Autonomous_Init() if this is the first time
                // we've entered autonomous_mode
                if (m_lastMode != Mode.Autonomous)
                {
                    LiveWindow.LiveWindow.SetEnabled(false);
                    // KBS NOTE: old code reset all PWMs and relays to "safe values"
                    // whenever entering autonomous mode, before calling
                    // "Autonomous_Init()"
                    AutonomousInit();
                    m_lastMode = Mode.Autonomous;
                }
                HAL_ObserveUserProgramAutonomous();
                AutonomousPeriodic();
            }
            else if (IsOperatorControl)
            {
                // call Teleop_Init() if this is the first time
                // we've entered teleop_mode
                if (m_lastMode != Mode.Teleop)
                {
                    LiveWindow.LiveWindow.SetEnabled(false);
                    TeleopInit();
                    m_lastMode = Mode.Teleop;
                }
                HAL_ObserveUserProgramTeleop();
                TeleopPeriodic();
            }
            else
            {
                // call TestInit() if we are now just entering test mode from either
                // a different mode or from power-on
                if (m_lastMode != Mode.Test)
                {
                    LiveWindow.LiveWindow.SetEnabled(true);
                    TestInit();
                    m_lastMode = Mode.Test;
                }
                HAL_ObserveUserProgramTest();
                TestPeriodic();
            }
            RobotPeriodic();
            SmartDashboard.SmartDashboard.UpdateValues();
            //LiveWindow.LiveWindow.UpdateValues();
        }

    }
}
