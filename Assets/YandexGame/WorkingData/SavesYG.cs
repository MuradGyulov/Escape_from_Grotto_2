
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;



        // Ваши сохранения
        public int completedLevels;
        public float soundsVolume = 0.5f;
        public float musicVolume = 0.14f;
        public float cameraSize = 4.5f;
        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива

    }
}
