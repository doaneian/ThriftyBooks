using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThriftyBooksEnums
{
    public enum condition { eRent = 0, eUsed, eNew };
    public enum returnCodes {  eSuccess = 0, eFail = -1 }
    public enum tableStatusCode { eEmpty = 0, eInitial, eExpanded}
}