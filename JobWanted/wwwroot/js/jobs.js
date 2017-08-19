var job = function () {
    //页面数据
    var _pageData = {
        pageIndex: 1,
        city: "上海",
        key: ".net",
        isGetJobsByZL: true,
        isGetJobsByLP: true,
        isGetJobsByQC: true,
        isGetJobsByBS: true,
    };

    return {
        init: function () {
            this.pageInit();
            this.bindEvent();
        },
        //独立方法
        method: {
            //加载招聘信息
            loadJobsInfo: function (type, city, key, index) {
                var tempHtml = "";
                var source = "", salaryType = "月薪";
                switch (type) {
                    case "GetJobsByZL":
                        source = "智联";
                        break;
                    case "GetJobsByLP":
                        source = "猎聘";
                        salaryType = "年薪";
                        break;
                    case "GetJobsByQC":
                        source = "前程";
                        break;
                    case "GetJobsByBS":
                        source = "BOSS";
                        break;
                    case "GetJobsByLG":
                        source = "拉勾";
                        break;
                }

                $.ajax({
                    url: "/api/jobs/" + type + "?city=" + city + "&key=" + key + "&index=" + index,
                    data: "",
                    success: function (sData) {
                        for (var i = 0; i < sData.length; i++) {
                            var job = sData[i];
                            tempHtml += ' <div class="jobinfo clearfix">\
                                        <div class="col clearfix">\
                                            <div class="col positionName">\
                                                <a onclick="event.stopPropagation();" class="info_url" href="'+ job.detailsUrl + '" target="_blank">' + job.positionName + '</a>\
                                            </div> \
                                            <div class="col corporateName">'+ job.corporateName + '</div>\
                                        </div>\
                                        <div class="col workingPlace">'+ job.workingPlace + '</div>\
                                        <div class="col releaseDate">'+ job.releaseDate + '</div>\
                                        <div class="col salaryType">'+ salaryType + '</div>\
                                        <div class="col salary">'+ job.salary + '</div>\
                                        <div class="col source">'+ source + '</div>\
                                    </div>';
                        }
                        $(".dataDiv").append(tempHtml);
                    }
                });
            },
            //重置信息
            resetInfo: function () {
                _pageData.pageIndex = this.queryString("index") || 0;
                _pageData.city = $(".place_a.select_place_a").text();
                _pageData.key = $(".jobKey").val();
                _pageData.isGetJobsByZL = $(".isGetJobsByZL").prop("checked");
                _pageData.isGetJobsByLP = $(".isGetJobsByLP").prop("checked");
                _pageData.isGetJobsByQC = $(".isGetJobsByQC").prop("checked");
                _pageData.isGetJobsByBS = $(".isGetJobsByBS").prop("checked");
                _pageData.isGetJobsByLG = $(".isGetJobsByLG").prop("checked");
                $(".dataDiv").html("");
            },
            //加载详细信息
            loadDetailsInfo: function (ele, type, url) {
                var urlType = "";
                switch (type) {
                    case "智联":
                        urlType = "GetDetailsInfoByZL";
                        break;
                    case "猎聘":
                        urlType = "GetDetailsInfoByLP";
                        break;
                    case "前程":
                        urlType = "GetDetailsInfoByQC";
                        break;
                    case "BOSS":
                        urlType = "GetDetailsInfoByBS";
                        break;
                    case "拉勾":
                        urlType = "GetDetailsInfoByLG";
                        break;
                }

                $.ajax({
                    url: "/api/jobs/" + urlType + "?url=" + url,
                    success: function (sData) {
                        if (sData) {
                            var tempHtml = "";
                            tempHtml += "<div class='detailsSketch detailsBlock'>" + (sData.experience || "");
                            tempHtml += "&nbsp&nbsp|&nbsp&nbsp" + (sData.education || "") + "";
                            tempHtml += "&nbsp&nbsp|&nbsp&nbsp" + (sData.companyNature || "") + "";
                            tempHtml += "&nbsp&nbsp|&nbsp&nbsp" + (sData.companySize || "") + "</div>";
                            tempHtml += "<div class='detailsBlock'><div class='detailsBlock-title'>职位描述：</div>" + (sData.requirement || "").trim() + "</div>";
                            if (sData.companyIntroduction)
                                tempHtml += "<div class='detailsBlock'><div class='detailsBlock-title'>公司简介：</div>" + (sData.companyIntroduction || "").trim() + "</div>";
                            $(ele).html(tempHtml);
                        }
                    }
                });
            },
            //QueryString
            queryString: function (name) {
                var url = encodeURI(location.search); //获取url中"?"符后的字串
                var theRequest = new Object();
                if (url.indexOf("?") != -1) {
                    var str = url.substr(1);
                    strs = str.split("&");
                    for (var i = 0; i < strs.length; i++) {
                        theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
                    }
                }
                return theRequest[name];
            }
        },
        //页面加载初始化
        pageInit: function () {

            var method = this.method;
            var jobTypes = "0-1-2-3-4-";//默认初始全部    
            //根据url中的类型
            var types = method.queryString("type") && method.queryString("type").split('-') || jobTypes.split("-");
            jobTypes = types.join('-');
            $(".jobKey").val(method.queryString("key") || _pageData.key);//根据URL中的关键字
            ////根据URL中的区域绑定
            $(".place_a.select_place_a").removeClass("select_place_a");
            $(".place_a").each(function (i, e) {
                if ($(this).text() === decodeURI(method.queryString("city") || _pageData.city)) {
                    $(this).addClass("select_place_a");
                }
            })
            //如果是更多区域
            if (!$(".place_a.select_place_a").length) {
                $(".temp_place_a").text(decodeURI(method.queryString("city") || _pageData.city)).addClass("select_place_a");
            }
            $(":checkbox").prop("checked", false);
            types.indexOf("0") >= 0 && $(".isGetJobsByZL").prop("checked", true);
            types.indexOf("1") >= 0 && $(".isGetJobsByQC").prop("checked", true);
            types.indexOf("2") >= 0 && $(".isGetJobsByLP").prop("checked", true);
            types.indexOf("3") >= 0 && $(".isGetJobsByBS").prop("checked", true);
            types.indexOf("4") >= 0 && $(".isGetJobsByLG").prop("checked", true);
            method.resetInfo();

            $.each(types, function (i, e) {
                if (e === "0")
                    method.loadJobsInfo("GetJobsByZL", _pageData.city, _pageData.key, _pageData.pageIndex);
                if (e === "1")
                    method.loadJobsInfo("GetJobsByQC", _pageData.city, _pageData.key, _pageData.pageIndex);
                if (e === "2")
                    method.loadJobsInfo("GetJobsByLP", _pageData.city, _pageData.key, _pageData.pageIndex);
                if (e === "3")
                    method.loadJobsInfo("GetJobsByBS", _pageData.city, _pageData.key, _pageData.pageIndex);
                if (e === "4")
                    method.loadJobsInfo("GetJobsByLG", _pageData.city, _pageData.key, _pageData.pageIndex);
            });

            history.pushState(null, null, location.href.split("?")[0] + "?type=" + jobTypes + "&city=" + _pageData.city + "&key=" + _pageData.key + "&index=" + _pageData.pageIndex);//塞入历史记录，并改变当前url
        },
        //事件绑定
        bindEvent: function () {

            var method = this.method;
            //自动加载信息
            var autoLoad = function () {
                var type = "";             
                if (_pageData.isGetJobsByZL) {
                    type += "0-";
                    method.loadJobsInfo("GetJobsByZL", _pageData.city, _pageData.key, _pageData.pageIndex);
                }
                if (_pageData.isGetJobsByQC) {
                    type += "1-";
                    method.loadJobsInfo("GetJobsByQC", _pageData.city, _pageData.key, _pageData.pageIndex);
                }
                if (_pageData.isGetJobsByLP) {
                    type += "2-";
                    method.loadJobsInfo("GetJobsByLP", _pageData.city, _pageData.key, _pageData.pageIndex);
                }
                if (_pageData.isGetJobsByBS) {
                    type += "3-";
                    method.loadJobsInfo("GetJobsByBS", _pageData.city, _pageData.key, _pageData.pageIndex);
                }
                if (_pageData.isGetJobsByLG) {
                    type += "4-";
                    method.loadJobsInfo("GetJobsByLG", _pageData.city, _pageData.key, _pageData.pageIndex);
                }
                //if (event && event.clientX) //用来判断是否是鼠标点击触发
                history.pushState(null, null, location.href.split("?")[0] + "?type=" + type + "&city=" + _pageData.city + "&key=" + _pageData.key + "&index=" + _pageData.pageIndex);//塞入历史记录，并改变当前url
            };

            //重新加载
            var reloadLoad = function () {
                method.resetInfo();
                autoLoad();
            }
            //点击 更多区域
            $(".btn-moreplace").click(function () {
                $(".hid_place_div").toggle();
                $(".hid_place_div").css("position", "absolute")
                    .css("width", $(".mytableSelect").css("width"))
                    .css("left", $(".mytableSelect")[0].offsetLeft - 1);
            });

            //点击热门区域
            $(".place_a").click(function () {
                $(".place_a.select_place_a").removeClass("select_place_a");
                $(this).addClass("select_place_a");
                reloadLoad();
            });

            //点击其他区域
            $(".hid_place_a").click(function () {
                debugger
                $(".hid_place_div").hide();
                $(".place_a.select_place_a").removeClass("select_place_a");
                $(".temp_place_a").text($(this).text()).addClass("select_place_a");
                reloadLoad();
            });

            //点击重新查询
            $(".btn-query").click(function () {
                reloadLoad();
            });

            //回车搜索
            $(".jobKey").keydown(function (event) {
                if (event.keyCode == 13)
                    reloadLoad();
            });

            //点击复选框时
            $(":checkbox").click(function () {
                reloadLoad();
            });

            //点击选中(加载详情)
            $("html").on("click", ".jobinfo", function () {

                $(".detailsInfo").not($(this).next()).addClass("displayNone");

                if ($(this).next().hasClass("detailsInfo")) {
                    if ($(this).next().hasClass("displayNone"))
                        $(this).next().removeClass("displayNone");
                    else
                        $(this).next().addClass("displayNone");
                }
                else {
                    $(this).after("<div class='detailsInfo'><div class='loading'>正在加载...</div></div>");
                    var url = $(this).find(".positionName .info_url").prop("href");
                    //加载详情
                    method.loadDetailsInfo($(this).next(), $(this).find(".source").text(), url);
                }

                if (!$(this).hasClass("chosen"))
                    $(this).addClass("chosen");
            });

            //鼠标移出事件
            $("html").on("mouseout", ".jobinfo", function () {
                if ($(this).hasClass("mouseover"))
                    $(this).removeClass("mouseover");
            });

            //鼠标进入事件
            $("html").on("mouseover", ".jobinfo", function () {
                if (!$(this).hasClass("mouseover"))
                    $(this).addClass("mouseover");
            });

            if (history.pushState) {
                window.addEventListener("popstate", function () {
                });
            }

            //滚动条
            $(window).scroll(function () {
                var scrollTop = $(window).scrollTop();
                var top = $(document).height() - $(window).height() - scrollTop;
                if (top == 0) {
                    _pageData.pageIndex++;
                    autoLoad();
                }
            });
        }
    };
}();

//页面加载完成
$(function () {
    job.init();
});