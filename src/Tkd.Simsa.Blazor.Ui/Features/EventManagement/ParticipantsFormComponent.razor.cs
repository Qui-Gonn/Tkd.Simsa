namespace Tkd.Simsa.Blazor.Ui.Features.EventManagement;

using MediatR;

using Microsoft.AspNetCore.Components;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;

public partial class ParticipantsFormComponent : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public required List<Participant> Participants { get; set; }

    [Inject]
    private IMediator Mediator { get; set; } = null!;

    private async Task<IEnumerable<Person>> FindPerson(string? searchValue, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchValue)
            || searchValue.Length < 2)
        {
            return [];
        }

        return await this.Mediator.Send(
            new GetItemsQuery<Person>(
                new QueryParameters<Person>
                {
                    FilterDescriptors =
                    [
                        new FilterDescriptor<Person>(FilterOperator.Contains, i => i.Name, searchValue)
                    ]
                }),
            cancellationToken);
    }
}