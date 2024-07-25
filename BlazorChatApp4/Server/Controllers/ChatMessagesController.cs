using BlazorChatApp4.Server.Services;
using Microsoft.AspNetCore.Mvc;
using BlazorChatApp4.Server.Data;

namespace BlazorChatApp4.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatMessagesController : ControllerBase
    {
        private readonly ChatMessageService _chatMessageService;

        public ChatMessagesController(ChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatMessage>>> GetMessages()
        {
            return await _chatMessageService.GetMessagesAsync();
        }
    }
}
