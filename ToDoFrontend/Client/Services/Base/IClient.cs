﻿namespace ToDoFrontend.Client.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}