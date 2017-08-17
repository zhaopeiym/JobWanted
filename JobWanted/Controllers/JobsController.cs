using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using AngleSharp.Parser.Html;
using System;
using System.Net.Http.Headers;
using Talk.Cache;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/*
 需要安装：
 1、AngleSharp 【html解析组件】
 2、System.Text.Encoding.CodePages 【Encoding.GetEncoding("GBK")编码提供程序】
*/

namespace JobWanted.Controllers
{
    [Route("api/[controller]/[action]")]
    public class JobsController : Controller
    {
        /// <summary>
        /// 获取智联信息(简要信息)
        /// </summary>
        /// <param name="city"></param>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<List<JobInfo>> GetJobsByZL(string city, string key, string index)
        {
            var keyString = city + key + index;
            var time = DateTime.Now.AddHours(1) - DateTime.Now;
            EasyCache<List<JobInfo>> str = new EasyCache<List<JobInfo>>(keyString, time);
            var data = str.GetData();
            if (data != null)
            {
                return data.Data;
            }

            var cityCode = CodesData.GetCityCode(RecruitEnum.智联招聘, city);
            string url = string.Format("http://sou.zhaopin.com/jobs/searchresult.ashx?jl={0}&kw={1}&p={2}", cityCode, key, index);
            using (HttpClient http = new HttpClient())
            {
                var htmlString = await http.GetStringAsync(url);
                HtmlParser htmlParser = new HtmlParser();
                var jobInfos = htmlParser.Parse(htmlString)
                    .QuerySelectorAll(".newlist_list_content table")
                    .Where(t => t.QuerySelectorAll(".zwmc a").FirstOrDefault() != null)
                    .Select(t => new JobInfo()
                    {
                        PositionName = t.QuerySelectorAll(".zwmc a").FirstOrDefault().TextContent,
                        CorporateName = t.QuerySelectorAll(".gsmc a").FirstOrDefault().TextContent,
                        Salary = t.QuerySelectorAll(".zwyx").FirstOrDefault().TextContent,
                        WorkingPlace = t.QuerySelectorAll(".gzdd").FirstOrDefault().TextContent,
                        ReleaseDate = t.QuerySelectorAll(".gxsj span").FirstOrDefault().TextContent,
                        DetailsUrl = t.QuerySelectorAll(".zwmc a").FirstOrDefault().Attributes.FirstOrDefault(f => f.Name == "href").Value,
                    })
                    .ToList();

                str.AddData(jobInfos);
                return jobInfos;
            }
        }

        /// <summary>
        /// 获取猎聘信息(简要信息)
        /// </summary>
        /// <param name="city"></param>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<List<JobInfo>> GetJobsByLP(string city, string key, string index)
        {
            var cityCode = CodesData.GetCityCode(RecruitEnum.猎聘网, city);
            string url = string.Format("http://www.liepin.com/zhaopin/?key={0}&dqs={1}&curPage={2}", key, cityCode, index);
            using (HttpClient http = new HttpClient())
            {
                var htmlString = await http.GetStringAsync(url);
                HtmlParser htmlParser = new HtmlParser();
                var jobInfos = htmlParser.Parse(htmlString)
                    .QuerySelectorAll("ul.sojob-list li")
                    .Where(t => t.QuerySelectorAll(".job-info h3 a").FirstOrDefault() != null)
                    .Select(t => new JobInfo()
                    {
                        PositionName = t.QuerySelectorAll(".job-info h3 a").FirstOrDefault().TextContent,
                        CorporateName = t.QuerySelectorAll(".company-name a").FirstOrDefault().TextContent,
                        Salary = t.QuerySelectorAll(".text-warning").FirstOrDefault().TextContent,
                        WorkingPlace = t.QuerySelectorAll(".area").FirstOrDefault().TextContent,
                        ReleaseDate = t.QuerySelectorAll(".time-info time").FirstOrDefault().TextContent,
                        DetailsUrl = t.QuerySelectorAll(".job-info h3 a").FirstOrDefault().Attributes.FirstOrDefault(f => f.Name == "href").Value,
                    })
                    .ToList();
                return jobInfos;
            }
        }

        /// <summary>
        /// 获取前程信息(简要信息)
        /// </summary>
        /// <param name="city"></param>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<List<JobInfo>> GetJobsByQC(string city, string key, string index)
        {
            var cityCode = CodesData.GetCityCode(RecruitEnum.前程无忧, city);
            string url = string.Format("http://search.51job.com/jobsearch/search_result.php?jobarea={0}&keyword={1}&curr_page={2}", cityCode, key, index);
            using (HttpClient http = new HttpClient())
            {
                var htmlBytes = await http.GetByteArrayAsync(url);
                //【注意】使用GBK需要 Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//注册编码提供程序
                var htmlString = Encoding.GetEncoding("GBK").GetString(htmlBytes);
                HtmlParser htmlParser = new HtmlParser();
                var jobInfos = (await htmlParser.ParseAsync(htmlString))
                    .QuerySelectorAll(".dw_table div.el")
                    .Where(t => t.QuerySelectorAll(".t1 span a").FirstOrDefault() != null)
                    .Select(t => new JobInfo()
                    {
                        PositionName = t.QuerySelectorAll(".t1 span a").FirstOrDefault().TextContent,
                        CorporateName = t.QuerySelectorAll(".t2 a").FirstOrDefault().TextContent,
                        Salary = t.QuerySelectorAll(".t3").FirstOrDefault().TextContent,
                        WorkingPlace = t.QuerySelectorAll(".t4").FirstOrDefault().TextContent,
                        ReleaseDate = t.QuerySelectorAll(".t5").FirstOrDefault().TextContent,
                        DetailsUrl = t.QuerySelectorAll(".t1 span a").FirstOrDefault().Attributes.FirstOrDefault(f => f.Name == "href").Value,
                    })
                    .ToList();
                return jobInfos;
            }
        }

        /// <summary>
        /// 获取拉勾信息(简要信息)
        /// </summary>
        /// <param name="city"></param>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<List<JobInfo>> GetJobsByLG(string city, string key, string index)
        {
            //不知道为什么抓不到数据？ https 的原因？？
            StringContent fromurlcontent = new StringContent("first=false&pn=" + index + "&kd=" + key);
            fromurlcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            fromurlcontent.Headers.Add("X-Requested-With", "XMLHttpRequest");

            var url = string.Format("https://www.lagou.com/jobs/positionAjax.json?city={0}&needAddtionalResult=false&isSchoolJob=0", city);
            using (HttpClient http = new HttpClient())
            {
                //MultipartFormDataContent MediaTypeHeaderValue  
                var responseMsg = await http.PostAsync(new Uri(url), fromurlcontent);
                var bytes = await responseMsg.Content.ReadAsByteArrayAsync();
                var html = Encoding.UTF8.GetString(bytes);
                return null;
            }
        }

        /// <summary>
        /// BOSS直聘信息(简要信息)
        /// </summary>
        /// <param name="city"></param>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<List<JobInfo>> GetJobsByBS(string city, string key, string index)
        {
            var keyString = city + key + index;
            var time = DateTime.Now.AddMinutes(3) - DateTime.Now;
            EasyCache<List<JobInfo>> str = new EasyCache<List<JobInfo>>(keyString, time);
            var data = str.GetData();
            if (data != null)
            {
                data.Data.Insert(0, new JobInfo()
                {
                    CorporateName = "缓存数据"
                });
                return data.Data;
            }

            var cityCode = CodesData.GetCityCode(RecruitEnum.BOSS, city);
            string url = string.Format("http://www.zhipin.com/c{0}/h_{0}/?query={1}&page={2}", cityCode, key, index);
            using (HttpClient http = new HttpClient())
            {
                var htmlString = await http.GetStringAsync(url);
                HtmlParser htmlParser = new HtmlParser();
                var jobInfos = htmlParser.Parse(htmlString)
                    .QuerySelectorAll(".job-list ul li")
                    .Where(t => t.QuerySelectorAll(".info-primary h3").FirstOrDefault() != null)
                    .Select(t => new JobInfo()
                    {
                        PositionName = t.QuerySelectorAll(".info-primary h3").FirstOrDefault().TextContent,
                        CorporateName = t.QuerySelectorAll(".company-text h3").FirstOrDefault().TextContent,
                        Salary = t.QuerySelectorAll(".info-primary h3 .red").FirstOrDefault().TextContent,
                        WorkingPlace = t.QuerySelectorAll(".info-primary p").FirstOrDefault().TextContent,
                        ReleaseDate = t.QuerySelectorAll(".job-time .time").FirstOrDefault().TextContent,
                        DetailsUrl = "http://www.zhipin.com" + t.QuerySelectorAll("a").FirstOrDefault().Attributes.FirstOrDefault(f => f.Name == "href").Value,
                    })
                    .ToList();
                str.AddData(jobInfos);
                return jobInfos;
            }
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<DetailsInfo> GetDetailsInfoByLP(string url)
        {
            using (HttpClient http = new HttpClient())
            {
                var htmlString = await http.GetStringAsync(url);
                HtmlParser htmlParser = new HtmlParser();
                var detailsInfo = htmlParser.Parse(htmlString)
                    .QuerySelectorAll(".wrap")
                    .Where(t => t.QuerySelectorAll(".job-qualifications").FirstOrDefault() != null)
                    .Select(t => new DetailsInfo()
                    {
                        Experience = t.QuerySelectorAll(".job-qualifications span")[1].TextContent,
                        Education = t.QuerySelectorAll(".job-qualifications span")[0].TextContent,
                        CompanyNature = t.QuerySelectorAll(".new-compintro li")[0].TextContent,
                        CompanySize = t.QuerySelectorAll(".new-compintro li")[1].TextContent,
                        Requirement = t.QuerySelectorAll(".job-item.main-message").FirstOrDefault().TextContent.Replace("职位描述：", ""),
                        CompanyIntroduction = t.QuerySelectorAll(".job-item.main-message.noborder").FirstOrDefault().TextContent,
                    })
                    .FirstOrDefault();
                return detailsInfo;
            }
        }

        public async Task<DetailsInfo> GetDetailsInfoByZL(string url)
        {
            using (HttpClient http = new HttpClient())
            {
                var htmlString = await http.GetStringAsync(url);
                HtmlParser htmlParser = new HtmlParser();
                var detailsInfo = htmlParser.Parse(htmlString)
                    .QuerySelectorAll(".terminalpage")
                    .Where(t => t.QuerySelectorAll(".terminalpage-left .terminal-ul li").FirstOrDefault() != null)
                    .Select(t => new DetailsInfo()
                    {
                        Experience = t.QuerySelectorAll(".terminalpage-left .terminal-ul li")[4].TextContent,
                        Education = t.QuerySelectorAll(".terminalpage-left .terminal-ul li")[5].TextContent,
                        CompanyNature = t.QuerySelectorAll(".terminalpage-right .terminal-company li")[1].TextContent,
                        CompanySize = t.QuerySelectorAll(".terminalpage-right .terminal-company li")[0].TextContent,
                        Requirement = t.QuerySelectorAll(".tab-cont-box .tab-inner-cont")[0].TextContent.Replace("职位描述：", ""),
                        CompanyIntroduction = t.QuerySelectorAll(".tab-cont-box .tab-inner-cont")[1].TextContent,
                    })
                    .FirstOrDefault();
                return detailsInfo;
            }
        }

        public async Task<DetailsInfo> GetDetailsInfoByBS(string url)
        {
            using (HttpClient http = new HttpClient())
            {
                var htmlString = await http.GetStringAsync(url);
                HtmlParser htmlParser = new HtmlParser();
                var detailsInfo = htmlParser.Parse(htmlString)
                    .QuerySelectorAll("#main")
                    .Where(t => t.QuerySelectorAll(".job-banner .info-primary p").FirstOrDefault() != null)
                    .Select(t => new DetailsInfo()
                    {
                        Experience = t.QuerySelectorAll(".job-banner .info-primary p").FirstOrDefault().TextContent,
                        //Education = t.QuerySelectorAll(".terminalpage-left .terminal-ul li")[5].TextContent,
                        CompanyNature = t.QuerySelectorAll(".job-banner .info-company p").FirstOrDefault().TextContent,
                        //CompanySize = t.QuerySelectorAll(".terminalpage-right .terminal-company li")[0].TextContent,
                        Requirement = t.QuerySelectorAll(".detail-content div.text").FirstOrDefault().TextContent.Replace("职位描述：", ""),
                        //CompanyIntroduction = t.QuerySelectorAll(".tab-cont-box .tab-inner-cont")[1].TextContent,
                    })
                    .FirstOrDefault();
                return detailsInfo;
            }
        }

        public async Task<DetailsInfo> GetDetailsInfoByQC(string url)
        {
            using (HttpClient http = new HttpClient())
            {
                var htmlBytes = await http.GetByteArrayAsync(url);
                //【注意】使用GBK需要 Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//注册编码提供程序
                var htmlString = Encoding.GetEncoding("GBK").GetString(htmlBytes);
                HtmlParser htmlParser = new HtmlParser();
                var detailsInfo = htmlParser.Parse(htmlString)
                    .QuerySelectorAll(".tCompanyPage")
                    .Where(t => t.QuerySelectorAll(".tBorderTop_box .t1 span").FirstOrDefault() != null)
                    .Select(t => new DetailsInfo()
                    {
                        //Experience = t.QuerySelectorAll(".terminalpage-left .terminal-ul li")[4].TextContent,
                        Education = t.QuerySelectorAll(".tBorderTop_box .t1 span")[0].TextContent,
                        CompanyNature = t.QuerySelectorAll(".msg.ltype")[0].TextContent,
                        //CompanySize = t.QuerySelectorAll(".terminalpage-right .terminal-company li")[0].TextContent,
                        Requirement = t.QuerySelectorAll(".bmsg.job_msg.inbox")[0].TextContent.Replace("职位描述：", ""),
                        CompanyIntroduction = t.QuerySelectorAll(".tmsg.inbox")[0].TextContent,
                    })
                    .FirstOrDefault();
                return detailsInfo;
            }
        }
    }
}
