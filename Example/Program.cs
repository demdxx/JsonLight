using System;
using JsonLight;

namespace Example
{
  class MainClass
  {
    public static void Main (string[] args)
    {
      string json = "{\"id\":6427608,\"name\":\"ngx_request_module\"," +
        "\"full_name\":\"demdxx/ngx_request_module\"," +
        "\"owner\":{\"login\":\"demdxx\",\"id\":286425,\"avatar_url\":" +
        "\"https://0.gravatar.com/avatar/d110d49aab427a104e9b1f9ffd3ce95c?d=h" +
        "ttps%3A%2F%2Fidenticons.github.com%2Fda1e8ed44f2f5b501f71ebfd20bc3229.p" +
        "ng\",\"gravatar_id\":\"d110d49aab427a104e9b1f9ffd3ce95c\",\"url\":\"ht" +
        "tps://api.github.com/users/demdxx\",\"html_url\":\"https://github.com/dem" +
        "dxx\",\"followers_url\":\"https://api.github.com" +
        "/users/demdxx/followers\",\"following_url\":\"htt" +
        "ps://api.github.com/users/demdxx/following{/other_use" +
        "r}\",\"gists_url\":\"https://api.github.com/users/" +
        "demdxx/gists{/gist_id}\",\"starred_url\":\"https:" +
        "//api.github.com/users/demdxx/starred{/owner}{/repo}" +
        "\",\"subscriptions_url\":\"https://api.github.com" +
        "/users/demdxx/subscriptions\",\"organizations_url\":" +
        "\"https://api.github.com/users/demdxx/orgs\",\"repos_u" +
        "rl\":\"https://api.github.com/users/demdxx/repos\", " +
        "\"events_url\":\"https://api.github.com/users/demdxx/e" +
        "vents{/privacy}\",\"received_events_url\":\"https://" +
        "api.github.com/users/demdxx/received_events\",\"type\":" +
        "\"User\"},\"private\":false,\"html_url\":\"https://gith" +
        "ub.com/demdxx/ngx_request_module\",\"description\":\"Ext" +
        "ernal http requests\",\"fork\":false,\"url\":\"https:/" +
        "/api.github.com/repos/demdxx/ngx_request_module\",\"fork" +
        "s_url\":\"https://api.github.com/repos/demdxx/ngx_reque" +
        "st_module/forks\",\"keys_url\":\"https://api.github.com/" +
        "repos/demdxx/ngx_request_module/keys{/key_id}\",\"collabor" +
        "ators_url\":\"https://api.github.com/repos/demdxx/ngx_req" +
        "uest_module/collaborators{/collaborator}\",\"teams_url\":" +
        "\"https://api.github.com/repos/demdxx/ngx_request_module/" +
        "teams\",\"hooks_url\":\"https://api.github.com/repos/dem" +
        "dxx/ngx_request_module/hooks\",\"issue_events_url\":\"htt" +
        "ps://api.github.com/repos/demdxx/ngx_request_module/issue" +
        "s/events{/number}\",\"events_url\":\"https://api.github." +
        "com/repos/demdxx/ngx_request_module/events\",\"assignees_url" +
        "\":\"https://api.github.com/repos/demdxx/ngx_request_module/" +
        "assignees{/user}\",\"branches_url\":\"https://api.github.com" +
        "/repos/demdxx/ngx_request_module/branches{/branch}\",\"tags_u" +
        "rl\":\"https://api.github.com/repos/demdxx/ngx_request_modul" +
        "e/tags\",\"blobs_url\":\"https://api.github.com/repos/demdxx" +
        "/ngx_request_module/git/blobs{/sha}\",\"git_tags_url\":\"http" +
        "s://api.github.com/repos/demdxx/ngx_request_module/git/tags{/" +
        "sha}\",\"git_refs_url\":\"https://api.github.com/repos/demdxx/n" +
        "gx_request_module/git/refs{/sha}\",\"trees_url\":\"https://api.gi" +
        "thub.com/repos/demdxx/ngx_request_module/git/trees{/sha}\",\"statu" +
        "ses_url\":\"https://api.github.com/repos/demdxx/ngx_request_module" +
        "/statuses/{sha}\",\"languages_url\":\"https://api.github.com/repos/" +
        "demdxx/ngx_request_module/languages\",\"stargazers_url\":\"https:/" +
        "/api.github.com/repos/demdxx/ngx_request_module/stargazers\",\"con" +
        "tributors_url\":\"https://api.github.com/repos/demdxx/ngx_request_" +
        "module/contributors\",\"subscribers_url\":\"https://api.github.com" +
        "/repos/demdxx/ngx_request_module/subscribers\",\"subscription_ur" +
        "l\":\"https://api.github.com/repos/demdxx/ngx_request_module/sub" +
        "scription\",\"commits_url\":\"https://api.github.com/repos/demdxx" +
        "/ngx_request_module/commits{/sha}\",\"git_commits_url\":\"https://" +
        "api.github.com/repos/demdxx/ngx_request_module/git/commits{/sh" +
        "a}\",\"comments_url\":\"https://api.github.com/repos/demdxx/ngx_r" +
        "equest_module/comments{/number}\",\"issue_comment_url\":\"https://a" +
        "pi.github.com/repos/demdxx/ngx_request_module/issues/comments/{num" +
        "ber}\",\"contents_url\":\"https://api.github.com/repos/demdxx/ngx_r" +
        "equest_module/contents/{+path}\",\"compare_url\":\"https://api.gith" +
        "ub.com/repos/demdxx/ngx_request_module/compare/{base}...{head}\",\"me" +
        "rges_url\":\"https://api.github.com/repos/demdxx/ngx_request_module/" +
        "merges\",\"archive_url\":\"https://api.github.com/repos/demdxx/ngx_re" +
        "quest_module/{archive_format}{/ref}\",\"downloads_url\":\"https://api" +
        ".github.com/repos/demdxx/ngx_request_module/downloads\",\"issues_url\":\"https:/" +
        "/api.github.com/repos/demdxx/ngx_request_module/issues{/number}\",\"pulls_" +
        "url\":\"https://api.github.com/repos/demdxx/ngx_request_module/pulls{/numb" +
        "er}\",\"milestones_url\":\"https://api.github.com/repos/demdxx/ngx_reques" +
        "t_module/milestones{/number}\",\"notifications_url\":\"https://api.githu" +
        "b.com/repos/demdxx/ngx_request_module/notifications{?since,all,participat" +
        "ing}\",\"labels_url\":\"https://api.github.com/repos/demdxx/ngx_request_" +
        "module/labels{/name}\",\"created_at\":\"2012-10-28T13:12:36Z\",\"updated" +
        "_at\":\"2013-01-12T21:02:33Z\",\"pushed_at\":\"2012-10-28T17:16:26Z\",\"g" +
        "it_url\":\"git://github.com/demdxx/ngx_request_module.git\",\"ssh_url\":\"git" +
        "@github.com:demdxx/ngx_request_module.git\",\"clone_url\":\"https://githu" +
        "b.com/demdxx/ngx_request_module.git\",\"svn_url\":\"https://github.com/de" +
        "mdxx/ngx_request_module\",\"homepage\":null,\"size\":164,\"watchers_coun" +
        "t\":0,\"language\":\"C\",\"has_issues\":true,\"has_downloads\":true,\"h" +
        "as_wiki\":true,\"forks_count\":0,\"mirror_url\":null,\"open_issues_coun" +
        "t\":0,\"forks\":0,\"open_issues\":0,\"watchers\":0,\"master_branch\":\"mas" +
        "ter\",\"default_branch\":\"master\",\"network_count\":0\n}";

      JObject j = JObject.DecodeObject (json);
      Console.WriteLine ("JSON Encode: " + j.ToString ());
    }
  }
}
