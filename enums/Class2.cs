using System;

namespace crosstraining.enums
{
    public class Class2
    {
        public MTDGenericWorkflowStatus Status { get; set; }

        public static implicit operator Class2(Class1 v)
        {
            try
            {
                return new Class2
                {
                    Status = (MTDGenericWorkflowStatus)Enum.Parse(typeof(MTDGenericWorkflowStatus), v.Status.ToString())
                };
            }
            catch (Exception)
            {
                throw new Exception("Cast not possible TRY 1");
            }
        }
    }
}
