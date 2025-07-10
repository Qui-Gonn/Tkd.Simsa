namespace Tkd.Simsa.Blazor.Ui.Features.EventManagement;

using MediatR;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;

public partial class ParticipantsFormComponent : ComponentBase
{
    [CascadingParameter]
    public MudForm MudForm { get; set; } = null!;

    [Parameter]
    public EventCallback<Participant> OnRemoveParticipant { get; set; }

    [Parameter]
    [EditorRequired]
    public required List<Participant> Participants { get; set; }

    [Parameter]
    public required EventCallback<List<Participant>> ParticipantsChanged { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private IMediator Mediator { get; set; } = null!;

    private Person? SelectedPerson { get; set; }

    private MudAutocomplete<Person> SelectPersonAutocompleteRef { get; set; } = null!;

    private async Task AddParticipant()
    {
        if (this.SelectedPerson is null)
        {
            return;
        }

        this.Participants.Add(
            new Participant
            {
                PersonId = this.SelectedPerson.Id,
                PersonInfo = this.SelectedPerson
            });
        await this.SelectPersonAutocompleteRef.ClearAsync();
        await this.ParticipantsChanged.InvokeAsync(this.Participants);
        await this.MudForm.Validate();
    }

    private async Task<IEnumerable<Person>> FindPerson(string? searchValue, CancellationToken cancellationToken = default)
    {
        searchValue ??= string.Empty;

        var filterFirstname = Filter.For<Person>().Property(i => i.Name.FirstName).Contains(searchValue);
        var filterLastname = Filter.For<Person>().Property(i => i.Name.LastName).Contains(searchValue);
        var queryParameters = QueryParameters.Create<Person>()
                                             .WithFilter(Filter.Or(filterFirstname, filterLastname))
                                             .WithSort(Sort.By<Person>(i => i.Name.LastName).ThenBy(i => i.Name.FirstName))
                                             .Build();
        var persons = await this.Mediator.Send(new GetItemsQuery<Person>(queryParameters), cancellationToken);
        return persons.ExceptBy(this.Participants.Select(p => p.PersonId), p => p.Id);
    }

    private async Task RemoveParticipant(Participant participant)
    {
        var confirmed = await this.DialogService.ShowMessageBox(
            $"Remove {participant.PersonInfo.Name} from event",
            new MarkupString(
                $"""Are you sure you want to remove <strong class="mud-secondary-text">{participant.PersonInfo.Name}</strong> from this event?"""),
            "Yes",
            "No");

        if (confirmed ?? false)
        {
            this.Participants.Remove(participant);
            await this.ParticipantsChanged.InvokeAsync(this.Participants);
            await this.MudForm.Validate();
        }
    }

    private async Task SelectedPersonChanged(Person? person)
    {
        this.SelectedPerson = person;
        await this.AddParticipant();
    }
}