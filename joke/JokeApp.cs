using System;
using Newtonsoft.Json;

namespace joke
{
	public class JokeApp
	{
		HttpClient client = new HttpClient();

        string jokeUrl = "https://official-joke-api.appspot.com/random_joke";


        public JokeApp()
		{}

		public void Run() {
            Console.Write("Enter how many jokes you want:");
            int amount = int.Parse(Console.ReadLine());

            Task<Joke> taskGettingAJoke = GetJokeAsync(jokeUrl);
            var j = taskGettingAJoke.Result;

            Console.WriteLine($"Type: {j.type}");
            Console.WriteLine($"Setup: {j.setup}");
            Console.WriteLine($"punchline: {j.punchline}");
            Console.WriteLine();

            Console.ReadKey();
        }

        async Task<Joke> GetJokeAsync(string endPointUrl)
        {
            Joke joke;
            HttpResponseMessage response = await client.GetAsync(endPointUrl);
            if (response.IsSuccessStatusCode)
            {
                joke = await response.Content.ReadAsAsync<Joke>();
            }
            else
                joke = new Joke();
            return joke;
        }
    }
}

