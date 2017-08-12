using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobWanted
{
    /// <summary>
    /// 简要信息
    /// </summary>
    public class JobInfo
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CorporateName { get; set; }

        /// <summary>
        /// 薪水
        /// </summary>
        public string Salary { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string WorkingPlace { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// 详情url
        /// </summary>
        public string DetailsUrl { get; set; }
    }

    /// <summary>
    /// 详情
    /// </summary>
    public class DetailsInfo
    {
        /// <summary>
        /// 学历要求
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 工作经验
        /// </summary>
        public string Experience { get; set; }
          
        /// <summary>
        /// 公司性质
        /// </summary>
        public string CompanyNature { get; set; }

        /// <summary>
        /// 公司规模
        /// </summary>
        public string CompanySize { get; set; }

        /// <summary>
        /// 详细要求 【职位描述】
        /// </summary>
        public string Requirement { get; set; }

        /// <summary>
        /// 公司介绍
        /// </summary>
        public string CompanyIntroduction { get; set; }
    }
}
