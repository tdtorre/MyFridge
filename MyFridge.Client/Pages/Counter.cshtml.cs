using Microsoft.AspNetCore.Blazor.Components;

namespace MyFridge.Client.Pages
{
    public class CounterModel : BlazorComponent
    {
        public int currentCount = 0;

        public void IncrementCount()
        {
            currentCount++;
        }
    }
}