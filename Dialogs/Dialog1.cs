using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstBot.Dialogs
{
    public class Dialog1 : Dialog
    {
        public Dialog1(string dialogId) : base(dialogId)
        {
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            await dc.Context.SendActivityAsync("Zidan SOM ZACAL A BUDEM OPAKOVAT kym nenapises get me out");
            return new DialogTurnResult(DialogTurnStatus.Waiting);
        }

        public override async  Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default)
        {
            if (dc.Context.Activity.Text == "get me out")
            {
                return await dc.EndDialogAsync("PICA", cancellationToken); 
            }

            if (dc.Context.Activity.Text == "prompt")
            {
                var promptMessage = MessageFactory.Text("Hej zidan voco go ??? ", "Hej zidan voco go???", InputHints.ExpectingInput);
                return await dc.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }

            if (dc.Context.Activity.Text == "redirect")
            {
                throw new RedirectDialogException();
            }

            await dc.Context.SendActivityAsync($"MUHAHAHAHAHA SI LEN MOJ a znam co pises: {dc.Context.Activity.Text}");

            return new DialogTurnResult(DialogTurnStatus.Waiting);
        }

        public override async Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null, CancellationToken cancellationToken = default)
        {
            await dc.Context.SendActivityAsync($"Si napisal: {result} a co vcil???");
            return new DialogTurnResult(DialogTurnStatus.Waiting);
        }
    }
}
