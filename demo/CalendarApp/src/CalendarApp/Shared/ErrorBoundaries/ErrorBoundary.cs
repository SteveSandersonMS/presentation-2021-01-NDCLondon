using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.AspNetCore.Components
{
    // WARNING: This is purely a proof-of-concept and should not be used in a real application.
    // It will harm performance significantly and doesn't handle all error cases. A production
    // implementation of this feature would be done quite differently.

    public class ErrorBoundary : ComponentBase
    {
        private Exception receivedException;

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment<Exception> ErrorContent { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (receivedException == null)
            {
                builder.OpenComponent<CascadingValue<ErrorBoundary>>(0);
                builder.AddAttribute(1, "Value", this);
                builder.AddAttribute(1, "ChildContent", ChildContent);
                builder.CloseComponent();
            }
            else if (ErrorContent != null)
            {
                builder.AddContent(2, ErrorContent(receivedException));
            }
            else
            {
                builder.OpenElement(3, "div");
                builder.AddAttribute(4, "class", "error");
                builder.AddContent(5, receivedException.ToString());
                builder.CloseElement();
            }
        }

        internal void NotifyException(Exception exception)
        {
            if (receivedException == null && exception != null)
            {
                receivedException = exception;
                StateHasChanged();
            }
        }
    }
}
