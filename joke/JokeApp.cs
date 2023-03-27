using System;
using Newtonsoft.Json;

namespace joke
{
    public class JokeApp
    {
        HttpClient client = new HttpClient();

        string jokeUrl = "https://official-joke-api.appspot.com/jokes/programming/random";


        public JokeApp()
        {}

       


        public void Run()
        {
            Console.Write("Enter how many jokes you want:");
            List<Task<Joke[]>> tasks = new();

            int amount = int.Parse(Console.ReadLine());

            for (int i = 0; i < amount; i++)
            {
                Task<Joke[]> aTaskGettingAJoke = GetJokeAsync(jokeUrl);
                tasks.Add(aTaskGettingAJoke);
            }

            foreach (Task<Joke[]> t in tasks)
            {
                Joke[] aJoke = t.Result;

                Console.WriteLine($"Type: {aJoke[0].Type}");
                Console.WriteLine($"Setup: {aJoke[0].Setup}");
                Console.WriteLine($"punchline: {aJoke[0].Punchline}");
                Console.WriteLine();
            }
            

            Console.ReadKey();
        }

        private async Task<Joke[]> GetJokeAsync(string endPointUrl)
        {
            Joke[] joke;
            HttpResponseMessage response = await client.GetAsync(endPointUrl);
            if (response.IsSuccessStatusCode)
            {
                joke = await response.Content.ReadAsAsync<Joke[]>();
            }
            else
                joke = new Joke[] {new Joke
                {
                    Type = "Error",
                    Setup = "",
                    Punchline = response.ReasonPhrase ?? ""
                } };
            return joke;
        }
    }
}

