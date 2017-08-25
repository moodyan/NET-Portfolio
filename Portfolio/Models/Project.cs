using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Project
    {
        public int id { get; set; }
        public Owner owner { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string description { get; set; }
        public bool @private { get; set; }
        public bool fork { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string archive_url { get; set; }
        public string assignees_url { get; set; }
        public string blobs_url { get; set; }
        public string branches_url { get; set; }
        public string clone_url { get; set; }
        public string collaborators_url { get; set; }
        public string comments_url { get; set; }
        public string commits_url { get; set; }
        public string compare_url { get; set; }
        public string contents_url { get; set; }
        public string contributors_url { get; set; }
        public string deployments_url { get; set; }
        public string downloads_url { get; set; }
        public string events_url { get; set; }
        public string forks_url { get; set; }
        public string git_commits_url { get; set; }
        public string git_refs_url { get; set; }
        public string git_tags_url { get; set; }
        public string git_url { get; set; }
        public string hooks_url { get; set; }
        public string issue_comment_url { get; set; }
        public string issue_events_url { get; set; }
        public string issues_url { get; set; }
        public string keys_url { get; set; }
        public string labels_url { get; set; }
        public string languages_url { get; set; }
        public string merges_url { get; set; }
        public string milestones_url { get; set; }
        public string mirror_url { get; set; }
        public string notifications_url { get; set; }
        public string pulls_url { get; set; }
        public string releases_url { get; set; }
        public string ssh_url { get; set; }
        public string stargazers_url { get; set; }
        public string statuses_url { get; set; }
        public string subscribers_url { get; set; }
        public string subscription_url { get; set; }
        public string svn_url { get; set; }
        public string tags_url { get; set; }
        public string teams_url { get; set; }
        public string trees_url { get; set; }
        public string homepage { get; set; }
        public object language { get; set; }
        public int forks_count { get; set; }
        public int stargazers_count { get; set; }
        public int watchers_count { get; set; }
        public int size { get; set; }
        public string default_branch { get; set; }
        public int open_issues_count { get; set; }
        public bool has_issues { get; set; }
        public bool has_wiki { get; set; }
        public bool has_pages { get; set; }
        public bool has_downloads { get; set; }
        public string pushed_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public static List<Project> GetStarredRepos(int amount)
    {
        RestClient client = new RestClient("https://api.github.com");
        RestRequest request = new RestRequest($"/search/repositories?q=user:moodyan&sort=stars&per_page={amount}", Method.GET);
        request.AddHeader("User-Agent", "moodyan");
        RestResponse response = new RestResponse();

        Task.Run(async () =>
        {
            response = await GetResponseContentAsync(client, request) as RestResponse;
        }).Wait();

        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
        List<Project> project = JsonConvert.DeserializeObject<List<Project>>(jsonResponse["items"].ToString());

        return project;
    }

    public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
    {
        var tcs = new TaskCompletionSource<IRestResponse>();
        theClient.ExecuteAsync(theRequest, response => {
            tcs.SetResult(response);
        });
        return tcs.Task;
    }
}
