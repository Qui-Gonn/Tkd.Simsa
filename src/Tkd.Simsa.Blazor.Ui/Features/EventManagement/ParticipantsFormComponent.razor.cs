namespace Tkd.Simsa.Blazor.Ui.Features.EventManagement;

using MediatR;

using Microsoft.AspNetCore.Components;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Common.Filtering;
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

        var queryParameters = new QueryParameters<Person>();
        var filterFirstname = FilterDescriptor.Property<Person>(i => i.Name.FirstName, searchValue, FilterOperators.Contains);
        queryParameters.AddFilter(filterFirstname);
        return await this.Mediator.Send(new GetItemsQuery<Person>(queryParameters), cancellationToken);
    }
}