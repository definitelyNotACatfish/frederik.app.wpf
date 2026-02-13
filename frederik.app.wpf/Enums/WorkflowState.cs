namespace frederik.app.wpf.Enums
{
    public class WorkflowState
    {
        public WorkflowState(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; private set; }

        public int Id { get; private set; }

        public IList<WorkflowState> WorkflowStates => new List<WorkflowState>()
        {
            Undefined,
            Init,
            RobotArmOnStationA,
            RobotArmOnStationB,
            RobotArmRotatedFromA2B,
            RobotArmRotatingFromB2A,
            LoadedWaferFromCassetteOnArm,
            UnloadedWaferFromArmIntoCassette
        };

        public static WorkflowState Undefined => new WorkflowState("Undefined", 0);

        public static WorkflowState RobotArmOnStationA => new WorkflowState("RobotArmOnStationA", 1);

        public static WorkflowState RobotArmOnStationB => new WorkflowState("RobotArmOnStationB", 2);

        public static WorkflowState RobotArmRotatedFromA2B => new WorkflowState("RobotArmRotatingFromA2B", 3);

        public static WorkflowState RobotArmRotatingFromB2A => new WorkflowState("RobotArmRotatingFromB2A", 4);

        public static WorkflowState LoadedWaferFromCassetteOnArm => new WorkflowState("LoadedWaferOnArm", 5);

        public static WorkflowState UnloadedWaferFromArmIntoCassette => new WorkflowState("UnloadedWaferFromArmIntoCassette", 6);

        public static WorkflowState Init => new WorkflowState("Init", 7);


        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != typeof(WorkflowState))
            { return false; }

            return ((WorkflowState)obj).Id == Id;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }
    }
}
