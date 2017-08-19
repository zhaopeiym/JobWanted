using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobWanted
{
    public class LagouData
    {
        public string success { get; set; }
        public string requestId { get; set; }
        public string msg { get; set; }
        public string resubmitToken { get; set; }
        public Content content { get; set; }
        public string code { get; set; }
    } 

    public class Content
    {
        public string pageNo { get; set; }
        public string pageSize { get; set; }
        public Hrinfomap hrInfoMap { get; set; }
        public Positionresult positionResult { get; set; }
    }

    public class Hrinfomap
    {
        public _3072507 _3072507 { get; set; }
        public _3415473 _3415473 { get; set; }
        public _2729529 _2729529 { get; set; }
        public _2734283 _2734283 { get; set; }
        public _1869098 _1869098 { get; set; }
        public _3418618 _3418618 { get; set; }
        public _3298426 _3298426 { get; set; }
        public _1871456 _1871456 { get; set; }
        public _3441270 _3441270 { get; set; }
        public _2497050 _2497050 { get; set; }
        public _3318460 _3318460 { get; set; }
        public _3495392 _3495392 { get; set; }
        public _2680870 _2680870 { get; set; }
        public _2159369 _2159369 { get; set; }
        public _3079854 _3079854 { get; set; }
    }

    public class _3072507
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _3415473
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _2729529
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _2734283
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _1869098
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _3418618
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _3298426
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _1871456
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _3441270
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _2497050
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _3318460
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _3495392
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _2680870
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _2159369
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class _3079854
    {
        public string userId { get; set; }
        public string positionName { get; set; }
        public string phone { get; set; }
        public string receiveEmail { get; set; }
        public string realName { get; set; }
        public string portrait { get; set; }
        public string userLevel { get; set; }
        public string canTalk { get; set; }
    }

    public class Positionresult
    {
        public string totalCount { get; set; }
        public Locationinfo locationInfo { get; set; }
        public Queryanalysisinfo queryAnalysisInfo { get; set; }
        public Strategyproperty strategyProperty { get; set; }
        public string hotLabels { get; set; }
        public string resultSize { get; set; }
        public Result[] result { get; set; }
    }

    public class Locationinfo
    {
        public string queryByGisCode { get; set; }
        public string businessZone { get; set; }
        public string locationCode { get; set; }
        public string city { get; set; }
        public string district { get; set; }
    }

    public class Queryanalysisinfo
    {
        public string industryName { get; set; }
        public string usefulCompany { get; set; }
        public string positionName { get; set; }
        public string companyName { get; set; }
    }

    public class Strategyproperty
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Result
    {
        public string companyId { get; set; }
        public string positionId { get; set; }
        public string industryField { get; set; }
        public string education { get; set; }
        public string workYear { get; set; }
        public string city { get; set; }
        public string positionAdvantage { get; set; }
        public string createTime { get; set; }
        public string salary { get; set; }
        public string positionName { get; set; }
        public string companySize { get; set; }
        public string companyShortName { get; set; }
        public string companyLogo { get; set; }
        public string financeStage { get; set; }
        public string jobNature { get; set; }
        public string approve { get; set; }
        public string[] companyLabelList { get; set; }
        public string[] positionLables { get; set; }
        public string[] industryLables { get; set; }
        public string district { get; set; }
        public string score { get; set; }
        public string[] businessZones { get; set; }
        public string imState { get; set; }
        public string lastLogin { get; set; }
        public string publisherId { get; set; }
        public string explain { get; set; }
        public string plus { get; set; }
        public string pcShow { get; set; }
        public string appShow { get; set; }
        public string deliver { get; set; }
        public string gradeDescription { get; set; }
        public string promotionScoreExplain { get; set; }
        public string firstType { get; set; }
        public string secondType { get; set; }
        public string isSchoolJob { get; set; }
        public string companyFullName { get; set; }
        public string adWord { get; set; }
        public string formatCreateTime { get; set; }
    }

}
