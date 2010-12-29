// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListCallsLoadHandler.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ListCallsLoadHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
{
    public class ListCallsLoadHandler
    {
        public const string QueryStringListType = "List";
        private readonly IRepository _repo;

        public ListCallsLoadHandler(IRepository repo)
        {
            _repo = repo;
        }

        public ListCallsResult Handle(NameValueCollection queryString, Person currentUser, SearchCriteria searchCriteria)
        {
            var listTypeString = queryString[QueryStringListType];
            ListCallsResult.CallListEnum listType;
            try
            {
                listType = (ListCallsResult.CallListEnum)Enum.Parse(typeof(ListCallsResult.CallListEnum), listTypeString);
            }
            catch (Exception)
            {
                throw new ApplicationException("Unrecognized call list type: " + listTypeString);
            }
            
            switch (listType)
            {
                case ListCallsResult.CallListEnum.Recent:
                    return new ListCallsResult(listType)
                    {
                        CallList = _repo.RecentCalls()
                    };

                case ListCallsResult.CallListEnum.Outstanding:
                    return new ListCallsResult(listType)
                    {
                        CallList = _repo.OutstandingCalls()
                    };

                case ListCallsResult.CallListEnum.Mine:
                    var callIDs = _repo.FindCallsForPerson(currentUser.ID);
                    List<Call> calls = callIDs.Select(_repo.FindCallByID).ToList();
                    calls.OrderByDescending(c => c.IncidentNumber);

                    return new ListCallsResult(listType)
                    {
                        CallList = calls
                    };

                case ListCallsResult.CallListEnum.Search:
                    return new ListCallsResult(listType)
                    {
                        CallList = _repo.FindCalls(searchCriteria)
                    };

                default:
                    throw new ApplicationException("Unrecognized call list type: " + listType);
            }
        }
    }

    public class ListCallsResult
    {
        public enum CallListEnum
        {
            Mine,
            Outstanding,
            Search,
            Recent
        }

        public CallListEnum ListType { get; private set; }
        public IEnumerable<Call> CallList { get; set; }
       
        public ListCallsResult(CallListEnum listType)
        {
            ListType = listType;
        }

        public ListCallsResult(CallListEnum listType, IEnumerable<Call> callList)
        {
            ListType = listType;
            CallList = callList;       
        }
    }
}