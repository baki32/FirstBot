using FirstBot.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstBot
{
    public static class Conversation
    {
        public static async Task SendAsync(this Dialog dialog, ITurnContext turnContext, IStatePropertyAccessor<DialogState> accessor, CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogSet dialogSet = new DialogSet(accessor);
            dialogSet.Add(dialog);
            dialogSet.Add(new Dialog1(nameof(Dialog1)));
            dialogSet.Add(new TextPrompt(nameof(TextPrompt)));
            DialogContext dialogContext = await dialogSet.CreateContextAsync(turnContext, cancellationToken);

            try
            {
                DialogTurnResult results = await dialogContext.ContinueDialogAsync(cancellationToken);
                if (results.Status == DialogTurnStatus.Empty)
                {
                    await dialogContext.BeginDialogAsync(dialog.Id, null, cancellationToken);
                }
            }
            catch (RedirectDialogException)
            {
                await dialogContext.CancelAllDialogsAsync();
                await dialogContext.BeginDialogAsync(dialog.Id, null, cancellationToken);
            }
            catch (Exception ex)
            {
                await turnContext.SendActivityAsync($"Pohubilo SE: {ex.ToString()}");
            }
        }
    }
}
