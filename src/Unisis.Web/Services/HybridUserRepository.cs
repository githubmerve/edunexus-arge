using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class HybridUserRepository : IUserRepository
{
    private readonly LocalDemoRepository _localRepository;
    private readonly FirestoreUserRepository? _firestoreRepository;

    public HybridUserRepository(IFirebaseInitializer initializer)
    {
        _localRepository = new LocalDemoRepository();

        initializer.Initialize();
        if (initializer.IsConfigured)
        {
            _firestoreRepository = new FirestoreUserRepository(initializer);
        }
    }

    public async Task<UserProfile?> GetUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var firestoreUser = await TryFirestore(
            repository => repository.GetUserAsync(userId, cancellationToken));

        if (firestoreUser is not null)
        {
            await _localRepository.SaveUserProfileAsync(firestoreUser, cancellationToken);
            return firestoreUser;
        }

        return await _localRepository.GetUserAsync(userId, cancellationToken);
    }

    public async Task SaveUserProfileAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        await _localRepository.SaveUserProfileAsync(profile, cancellationToken);
        await TryFirestore(repository => repository.SaveUserProfileAsync(profile, cancellationToken));
    }

    public async Task SaveIssueReportAsync(IssueReport report, CancellationToken cancellationToken = default)
    {
        await _localRepository.SaveIssueReportAsync(report, cancellationToken);
        await TryFirestore(repository => repository.SaveIssueReportAsync(report, cancellationToken));
    }

    public async Task SaveExcuseRequestAsync(ExcuseRequest request, CancellationToken cancellationToken = default)
    {
        await _localRepository.SaveExcuseRequestAsync(request, cancellationToken);
        await TryFirestore(repository => repository.SaveExcuseRequestAsync(request, cancellationToken));
    }

    public async Task SaveAvailabilitySlotAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default)
    {
        await _localRepository.SaveAvailabilitySlotAsync(slot, cancellationToken);
        await TryFirestore(repository => repository.SaveAvailabilitySlotAsync(slot, cancellationToken));
    }

    public async Task SaveQrSessionAsync(QrLoginSession session, CancellationToken cancellationToken = default)
    {
        await _localRepository.SaveQrSessionAsync(session, cancellationToken);
        await TryFirestore(repository => repository.SaveQrSessionAsync(session, cancellationToken));
    }

    public async Task<QrLoginSession?> GetQrSessionByHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        var firestoreSession = await TryFirestore(
            repository => repository.GetQrSessionByHashAsync(tokenHash, cancellationToken));

        if (firestoreSession is not null)
        {
            await _localRepository.SaveQrSessionAsync(firestoreSession, cancellationToken);
            return firestoreSession;
        }

        return await _localRepository.GetQrSessionByHashAsync(tokenHash, cancellationToken);
    }

    private async Task TryFirestore(Func<FirestoreUserRepository, Task> action)
    {
        if (_firestoreRepository is null)
        {
            return;
        }

        try
        {
            await action(_firestoreRepository);
        }
        catch
        {
        }
    }

    private async Task<T?> TryFirestore<T>(Func<FirestoreUserRepository, Task<T?>> action)
    {
        if (_firestoreRepository is null)
        {
            return default;
        }

        try
        {
            return await action(_firestoreRepository);
        }
        catch
        {
            return default;
        }
    }
}
