// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;

namespace DAL.Core
{
    public enum Gender
    {
        None,
        Female,
        Male
    }

    public enum DisclosureTypes
    {
        BankDiscosure = 1
    }

    public enum TransactionStatuses
    {
        /// <summary>
        /// id = 1
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Id = 2
        /// </summary>
        Completed = 2,
        /// <summary>
        /// Id = 3
        /// </summary>
        Bounced = 3,
        /// <summary>
        /// Id = 4
        /// </summary>
        Failed = 4
    }

    public enum ImportTypes
    {
        /// <summary>
        /// Id = 1
        /// </summary>
        RoutingNumbers = 1,
        Zipcodes = 2,
    }
}
