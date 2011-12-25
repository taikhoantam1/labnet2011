using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModel.Constant
{
    public enum ViewMode
    {
        Create=1,
        Edit=2,
        Detail=3,
    }
    public enum SearchTypeEnum
    {
        Contains,
        Word,
    }

    public enum AnalysisStatusEnum
    {
        New=  1,// Mới (Default)
        HaveResult=2,// Đã có kết quả
        Approved=3//Đã xác minh

    }

    public enum LabExaminationStatusEnum
    {
        New=1
    }
}