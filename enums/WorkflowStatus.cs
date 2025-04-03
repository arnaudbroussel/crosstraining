namespace crosstraining.enums
{
    public enum WorkflowStatus
    {
        /// <summary>
        /// Initialising
        /// </summary>
        Initialising,

        /// <summary>
        /// Complete
        /// </summary>
        Complete,

        /// <summary>
        /// Failed
        /// </summary>
        Failed,

        /// <summary>
        /// Started
        /// </summary>
        Started,

        /// <summary>
        /// ContextBuilderComplete
        /// </summary>
        ContextBuilderComplete,

        /// <summary>
        /// BusinessInformationComplete
        /// </summary>
        BusinessInformationComplete,

        /// <summary>
        /// BusinessInformationCompletePolledFile
        /// </summary>
        BusinessInformationCompletePolledFile,

        /// <summary>
        /// BusinessInformationCompletePolledFileUploadedFile
        /// </summary>
        BusinessInformationCompletePolledFileUploadedFile,

        /// <summary>
        /// AntiFraudHeaderComplete
        /// </summary>
        AntiFraudHeaderComplete,

        /// <summary>
        /// AntiFraudHeaderCompletePolledFile
        /// </summary>
        AntiFraudHeaderCompletePolledFile,

        /// <summary>
        /// AntiFraudHeaderCompletePolledFileUploadedFile
        /// </summary>
        AntiFraudHeaderCompletePolledFileUploadedFile,

        /// <summary>
        /// CreateReportComplete
        /// </summary>
        CreateReportComplete,

        /// <summary>
        /// PollReportComplete
        /// </summary>
        PollReportComplete
    }
}