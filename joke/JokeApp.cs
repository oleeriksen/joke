using System;
using Newtonsoft.Json;

namespace joke
{
    public class JokeApp
    {
        HttpClient client = new HttpClient();

        string jokeUrl = "https://official-joke-api.appspot.com/random_joke";


        public JokeApp()
        { }

        public void Run()
        {
            Console.Write("Enter how many jokes you want:");
            int amount = int.Parse(Console.ReadLine());


            Task<Joke> aTaskGettingAJoke = GetJokeAsync(jokeUrl);
            Joke aJoke = aTaskGettingAJoke.Result;

            Console.WriteLine($"Type: {aJoke.Type}");
            Console.WriteLine($"Setup: {aJoke.Setup}");
            Console.WriteLine($"punchline: {aJoke.Punchline}");
            Console.WriteLine();


            Console.ReadKey();
        }

        private async Task<Joke> GetJokeAsync(string endPointUrl)
        {
            Joke joke;
            HttpResponseMessage response = await client.GetAsync(endPointUrl);
            if (response.IsSuccessStatusCode)
            {
                joke = await response.Content.ReadAsAsync<Joke>();
            }
            else
                joke = new Joke
                {
                    Type = "Error",
                    Setup = "",
                    Punchline = response.ReasonPhrase ?? ""
                };
            return joke;
        }
    }
}

