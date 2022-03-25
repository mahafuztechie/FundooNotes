// ----------------------------------------------------------------------------------------------------------
// <copyright file="ResponseModel.cs" company="Bridgelabz"> 
// Copyright © 2021 Company="BridgeLabz" 
// </copyright> 
// <creator name="Harshitha Solleti"/> 
// ----------------------------------------------------------------------------------------------------------

namespace CommonLayer.Model
{
    /// <summary>
    /// Response Model Class contains the status, message and data for output
    /// </summary>
    /// <typeparam name="T">Used for Data</typeparam>
    public class ExceptionModel<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the output is executed 
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data
        /// </summary>
        public T Data { get; set; }
    }
}