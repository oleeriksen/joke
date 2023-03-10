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


        public void Run()
        {
            Console.Write("Enter how many jokes you want:");
            List<Task<Joke>> tasks = new();

            int amount = int.Parse(Console.ReadLine());

            for (int i = 0; i < amount; i++)
            {
                Task<Joke> aTaskGettingAJoke = GetJokeAsync(jokeUrl);
                tasks.Add(aTaskGettingAJoke);
            }

            foreach (Task<Joke> t in tasks)
            {
                Joke aJoke = t.Result;

                Console.WriteLine($"Type: {aJoke.Type}");
                Console.WriteLine($"Setup: {aJoke.Setup}");
                Console.WriteLine($"punchline: {aJoke.Punchline}");
                Console.WriteLine();
            }
            

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

