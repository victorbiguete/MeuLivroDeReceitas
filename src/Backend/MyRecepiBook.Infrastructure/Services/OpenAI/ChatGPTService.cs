using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Enum;
using MyRecipeBook.Domain.Services.OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Services.OpenAI
{
    public class ChatGPTService : IGenerateRecipeAI
    {
        private readonly ChatClient _chatClient;

        public ChatGPTService(ChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<GenerateRecipeDto> Generate(IList<string> ingredients)
        {
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(ResourceOpenAI.STARTING_GENERATE_RECIPE),
                new UserChatMessage(string.Join(";", ingredients))
            };

            var completion = await _chatClient.CompleteChatAsync(messages);

            var responseList = completion.Value.Content[0].Text
                .Split("\n")
                .Where(response => !response.Trim().Equals(string.Empty))
                .Select(item => item.Replace("[", "").Replace("]", ""))
                .ToList();

            var step = 1;

            return new GeneratedRecipeDto
            {
                Title = responseList[0],
                CookingTime = (CookingTime)Enum.Parse(typeof(CookingTime), responseList[1]),
                Ingredients = responseList[2].Split(";"),
                Instructions = responseList[3].Split("@").Select(instruction => new GeneratedInstructionDto
                {
                    Text = instruction.Trim(),
                    Step = step++
                }).ToList()
            };
        }
    }
}
