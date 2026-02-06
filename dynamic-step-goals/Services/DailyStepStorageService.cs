using dynamic_step_goals.Models;
using System.Text.Json;

namespace dynamic_step_goals.Services
{
    public class DailyStepStorageService
    {
        private readonly string _filePath;

        public DailyStepStorageService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "daily_steps.json");
        }

        public async Task<List<DailyStepEntry>> GetAllAsync()
        {
            return await LoadInternalAsync();
        }

        public async Task<DailyStepEntry?> GetByDateAsync(DateOnly date)
        {
            var entries = await LoadInternalAsync();
            return entries.FirstOrDefault(e => e.Date == date);
        }

        public async Task AddOrUpdateAsync(DailyStepEntry entry)
        {
            var entries = await LoadInternalAsync();

            var existingIndex = entries.FindIndex(e => e.Date == entry.Date);

            if (existingIndex >= 0)
                entries[existingIndex] = entry;
            else
                entries.Add(entry);

            await SaveInternalAsync(entries);
        }

        #region internal Helper

        private async Task<List<DailyStepEntry>> LoadInternalAsync()
        {
            if (!File.Exists(_filePath))
                return new List<DailyStepEntry>();

            try
            {
                var json = await File.ReadAllTextAsync(_filePath);

                return JsonSerializer.Deserialize<List<DailyStepEntry>>(json)
                       ?? new List<DailyStepEntry>();
            }
            catch
            {
                return new List<DailyStepEntry>();
            }
        }

        private async Task SaveInternalAsync(List<DailyStepEntry> entries)
        {
            var json = JsonSerializer.Serialize(
                entries,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            await File.WriteAllTextAsync(_filePath, json);
        }

        #endregion
    }
}
