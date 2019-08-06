using FirstBot.Dialogs;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstBot.Dialogs
{
    public class RootDialog : Dialog
    {
        private LuisRecognizer _recognizer;

        public RootDialog(string dialogId, LuisRecognizer recognizer) : base(dialogId)
        {
            _recognizer = recognizer;
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            await dc.Context.SendActivityAsync("Recognizing what you have said ZIDAN vyjebans");
            var result = await _recognizer.RecognizeAsync(dc.Context, cancellationToken);
            return await dc.BeginDialogAsync(nameof(Dialog1), options, cancellationToken);
        }

        public override Task<DialogTurnResult> ContinueDialogAsync(DialogContext outerDc, CancellationToken cancellationToken = default)
        {
            
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete, "Root DONE"));
        }

        public override async Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null, CancellationToken cancellationToken = default)
        {
            await dc.Context.SendActivityAsync("How can I help you next?");
            return await dc.EndDialogAsync("KOKOOOOOOOOOT");
        }
    }
}
