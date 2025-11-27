/*
 * Автор: Дмитрий Антипанов
 * Скрипт: EventLD
 * Назначение: Центральный класс событий и параметров для ландшафтного редактора.
 * Описание:
 * - Хранит глобальные параметры кистей, материалов, режима и состояния редактора.
 * - Объявляет делегаты и события для передачи данных между компонентами.
 * - Предоставляет методы для вызова событий и обновления параметров.
 * 
 * Используется для синхронизации UI, кистей и логики редактора ландшафта.
 */

using LD;
using System;
using TerrainCreator;
using UnityEngine;
namespace LD
{
    public class EventLD
    {
        #region Переменные
        public static DataLDObject DataLDObject;
        public static ObjectCreator ObjectCreator;
        public static CreatorT CreatorT;
        //Номер сохранения
        public static int NumSave;
        //Уровень воды
        public static float WaterHeight;
        //новая игра
        public static bool NewGame;
        //GeneratorMap
        public static bool RedChannelActiv;
        public static bool InPerlinRed;
        public static int TypeMapRed;
        public static float MinHeightRed;
        public static float MaxHeightRed;
        public static float ForceColorRed;
        public static float AngleMinRed;
        public static float AngleMaxRed;
        public static float ForcePerlinRed;
        public static float ScalePerlinRed;

        public static bool GreenChannelActiv;
        public static bool InPerlinGreen;
        public static int TypeMapGreen;
        public static float MinHeightGreen;
        public static float MaxHeightGreen;
        public static float ForceColorGreen;
        public static float AngleMinGreen;
        public static float AngleMaxGreen;
        public static float ForcePerlinGreen;
        public static float ScalePerlinGreen;

        public static bool BlueChannelActiv;
        public static bool InPerlinBlue;
        public static int TypeMapBlue;
        public static float MinHeightBlue;
        public static float MaxHeightBlue;
        public static float ForceColorBlue;
        public static float AngleMinBlue;
        public static float AngleMaxBlue;
        public static float ForcePerlinBlue;
        public static float ScalePerlinBlue;

        public static EnumModeLD EnumModeLD;
        public static bool Magnet;
        public static bool Vertical;
        public static bool Randomize = false;
        public static bool ModeTextActiv = false;
        public static bool HeightCameraPanelActiv = false;
        public static bool Environment;
        //кисть текстурирования
        public static float BrashTexturesWidth;
        public static float BrashTexturesForce;
        //кисть деформ ландшафта
        public static string LandscapeHeigntName;
        public static Texture2D LandscapeHeigntTexture;
        public static float LandscapeHeigntBrushSize;
        public static float LandscapeHeigntBrushHeight;
        public static float LandscapeHeigntBrushSmoothing;
        public static float BrushSmoothingIntensity;
        //Кисть делет
        public static float BrushSizeDelete;
        // массовая кисть
        public static float BrashMassSizeWidth;
        public static float BrashMassSizeDepth;
        public static float BrashMassNumberOfObjects;
        //Вращение кисти
        public static bool RotateBrush = false;
        //Высота камеры
        public static float HeightCameraWheel;
        //Материалы террайна
        public static int NumMaterialRed;
        public static int NumMaterialGreen;
        public static int NumMaterialBlue;
        public static int NumMaterialBlack;
        //Положение FPSExplorer
        public static Vector3 LDPosition;

        #endregion
        #region Делегаты
        public delegate void EventLDTexture(Texture2D param);
        public delegate void EventLDBool(bool param);
        public delegate void EventLDInt(int param);
        public delegate void EventLDFloat(float param);
        public delegate void EventLDVoid();
        public delegate void EventLDEnumModeLD(EnumModeLD param);
        public delegate void EventLDString(string param);
        public delegate void EventLDColor(Color param);
        public delegate void EventLDMaterials(EnumChannelMaterials enumChannelMaterials, int numMaterials);
        #endregion
        #region Ивенты
        public static event Action DataLDObjectLoading;
        public static event Action CreatorTLoading;
        public static event Action ObjectCreatorLoading;

        public static event EventLDVoid SaveLDEvent;
        public static event EventLDVoid LoadLDEvent;

        public static event EventLDBool FPSExplorerActivEvent;
        public static event EventLDEnumModeLD EnumModeLDEvent;
        public static event EventLDString NameObjectGrupEvent;

        public static event EventLDBool MagnetActivEvent;
        public static event EventLDBool VerticalActivEvent;
        public static event EventLDBool RandomizeActivEvent;
        public static event EventLDVoid RandomizeRunEvent;
        public static event EventLDInt MouseDownEvent;
        public static event EventLDInt KeyboardUpEvent;
        public static event EventLDFloat BrushSizeEvent;
        public static event EventLDFloat LeftShiftWheelEvent;
        public static event EventLDBool ModeTextEvent;
        public static event EventLDFloat HeightCameraWheelEvent;
        public static event EventLDBool HeightCameraPanelEvent;
        public static event EventLDFloat LeftControlWheelEvent;
        public static event EventLDFloat LeftAltWheelEvent;
        public static event EventLDBool RotateBrushEvent;
        public static event EventLDBool EnvironmentEvent;
        //Кисть текстурирования
        public static event EventLDFloat BrashTexturesWidthEvent;
        public static event EventLDFloat BrashTexturesForceEvent;
        //Кисть деформации Ландшафта
        public static event EventLDTexture LandscapeHeigntTextureEvent;
        public static event EventLDFloat LandscapeHeigntBrushSizeEvent;
        public static event EventLDFloat LandscapeHeigntBrushHeightEvent;
        public static event EventLDFloat LandscapeHeigntBrushSmoothingEvent;
        public static event EventLDString LandscapeHeigntNameEvent;
        public static event EventLDFloat BrushSmoothingIntensityEvent;
        //кисть удаления объектов
        public static event EventLDFloat SizeBrushDeletEvent;
        // кисть массовая
        public static event EventLDFloat BrashMassSizeWidthEvent;
        public static event EventLDFloat BrashMassSizeDepthEvent;
        public static event EventLDFloat BrashMassNumberOfObjectsEvent;
        // Отключение панелей
        public static event EventLDVoid PanelsNoActivEvent;
        //передача параметров в Paint
        public static event EventLDTexture PaintMasksEvent;
        public static event EventLDColor PaintColorEvent;
        //GeneratorMap
        public static event EventLDVoid GeneratorMapLoadEvent;
        public static event EventLDVoid GeneratorMapActivEvent;

        // передача какой материал нужно вставить
        public static event EventLDMaterials MaterialsEvent;
        //Активация панели выбор режима
        public static event EventLDVoid ActivPanelModeEvent;
        // загрузка панели генератора
        public static event EventLDVoid SettingGeneratorLoadEvent;
        //Созранение проекта
        public static event EventLDString SaveProjectEvent;
        //Сохранение объектов проекта
        public static event EventLDString SaveProjectObjectsEvent;
        //Загрузка объектов проекта
        public static event EventLDString LoadProjectObjectsEvent;

        #endregion
        #region Функции
        //Загрузка объектов проекта

        public static void OnDataLDObjectLoading() => DataLDObjectLoading?.Invoke();
        public static void OnCreatorTLoading() => CreatorTLoading?.Invoke();

        public static void OnObjectCreatorLoading() => ObjectCreatorLoading?.Invoke();

        public static void LoadProjectObjects(string param)
        {
            LoadProjectObjectsEvent?.Invoke(param);
        }
        //Сохранение объектов проекта
        public static void SaveProjectObjects(string param) { SaveProjectObjectsEvent?.Invoke(param); }
        //Созранение проекта
        public static void SaveProject(string param) { SaveProjectEvent?.Invoke(param); }
        // загрузка панели генератора
        public static void SettingGeneratorLoad() { SettingGeneratorLoadEvent?.Invoke(); }
        //Активация панели выбор режима
        public static void ActivPanelMode() { ActivPanelModeEvent?.Invoke(); }
        //GeneratorMap
        public static void GeneratorMapActivParam() { GeneratorMapActivEvent?.Invoke(); }
        public static void GeneratorMapLoadParam() { GeneratorMapLoadEvent?.Invoke(); }
        public static void MaterialsParam(EnumChannelMaterials enumChannelMaterials, int numMaterials)
        {
            MaterialsEvent?.Invoke(enumChannelMaterials, numMaterials);
            switch (enumChannelMaterials)
            {
                case EnumChannelMaterials.Red:
                    NumMaterialRed = numMaterials;
                    break;
                case EnumChannelMaterials.Green:
                    NumMaterialGreen = numMaterials;
                    break;
                case EnumChannelMaterials.Blue:
                    NumMaterialBlue = numMaterials;
                    break;
                case EnumChannelMaterials.Black:
                    NumMaterialBlack = numMaterials;
                    break;
            }
        }
        //передача параметров в Paint
        public static void PaintColorParam(Color param) { PaintColorEvent?.Invoke(param); }
        public static void PaintMasksParam(Texture2D param) { PaintMasksEvent?.Invoke(param); }
        // Отключение панелей
        public static void PanelsNoActiv() { PanelsNoActivEvent?.Invoke(); }

        //Кисть текстурирования
        public static void BrashTexturesWidthParam(float param)
        {
            BrashTexturesWidthEvent?.Invoke(param);

        }
        public static void BrashTexturesForceParam(float param)
        {
            BrashTexturesForceEvent?.Invoke(param);

        }
        // Environment
        public static void EnvironmentParam(bool param)
        {
            EnvironmentEvent?.Invoke(param);
            Environment = param;
        }
        // кисть массовая
        public static void BrashMassSizeWidthParam(float param)
        {
            BrashMassSizeWidthEvent?.Invoke(param);
            BrashMassSizeWidth = param;
        }
        public static void BrashMassSizeDepthParam(float param)
        {
            BrashMassSizeDepthEvent?.Invoke(param);
            BrashMassSizeDepth = param;
        }
        public static void BrashMassNumberOfObjectsParam(float param)
        {
            BrashMassNumberOfObjectsEvent?.Invoke(param);
            BrashMassNumberOfObjects = param;
        }
        // кисть делете
        public static void SizeBrushDelet(float param)
        {
            SizeBrushDeletEvent?.Invoke(param);
            BrushSizeDelete = param;

        }
        // нажатие клавиш 1 2 3 4
        public static void KeyboardUp(int param) { KeyboardUpEvent?.Invoke(param); }
        //Кисть деформации Ландшафта
        public static void LandscapeHeigntNameParam(string param)
        {
            LandscapeHeigntName = param;
            LandscapeHeigntNameEvent?.Invoke(param);
        }
        public static void LandscapeHeigntTextureParam(Texture2D param)
        {
            LandscapeHeigntTextureEvent?.Invoke(param);
            LandscapeHeigntTexture = param;
        }
        public static void LandscapeHeigntBrushSizeParam(float param)
        {
            LandscapeHeigntBrushSizeEvent?.Invoke(param);
            LandscapeHeigntBrushSize = param;
        }
        public static void LandscapeHeigntBrushHeightParam(float param)
        {
            LandscapeHeigntBrushHeightEvent?.Invoke(param);
            LandscapeHeigntBrushHeight = param;
        }
        public static void LandscapeHeigntBrushSmoothingParam(float param)
        {
            LandscapeHeigntBrushSmoothingEvent?.Invoke(param);
            LandscapeHeigntBrushSmoothing = param;
        }
        public static void BrushSmoothingIntensityParam(float param)
        {
            BrushSmoothingIntensityEvent?.Invoke(param);
            BrushSmoothingIntensity = param;
        }
        //LeftAlt+ колесо
        public static void LeftAltWheel(float param)
        {
            if (EventLD.EnumModeLD == EnumModeLD.Objects)
            {
                BrashMassNumberOfObjects += param;
                if (BrashMassNumberOfObjects > 100f) BrashMassNumberOfObjects = 100f;
                if (BrashMassNumberOfObjects < 5f) BrashMassNumberOfObjects = 5f;
                BrashMassNumberOfObjectsParam(BrashMassNumberOfObjects);
            }
            if (EventLD.EnumModeLD == EnumModeLD.LandscapeHeight)
            {
                LandscapeHeigntBrushSmoothing += param;
                if (LandscapeHeigntBrushSmoothing > 100f) LandscapeHeigntBrushSmoothing = 100f;
                if (LandscapeHeigntBrushSmoothing < -100f) LandscapeHeigntBrushSmoothing = -100f;
                LandscapeHeigntBrushSmoothingParam(LandscapeHeigntBrushSmoothing);
            }
            if (EventLD.EnumModeLD == EnumModeLD.BrashSmoothing)
            {
                BrushSmoothingIntensity += param / 10f;
                if (BrushSmoothingIntensity > 1f) BrushSmoothingIntensity = 1f;
                if (BrushSmoothingIntensity < 0f) BrushSmoothingIntensity = 0f;
                BrushSmoothingIntensityParam(BrushSmoothingIntensity);
            }
            LeftAltWheelEvent?.Invoke(param);
        }

        //LeftShift+ колесо
        public static void LeftShiftWheel(float param)
        {
            if (EventLD.EnumModeLD == EnumModeLD.LandscapeTexture)
            {
                BrashTexturesWidth += param;
                if (BrashTexturesWidth > 100f) BrashTexturesWidth = 100f;
                if (BrashTexturesWidth < 5f) BrashTexturesWidth = 5f;
                BrashTexturesWidthParam(BrashTexturesWidth);
            }
            if (EventLD.EnumModeLD == EnumModeLD.Lastic)
            {
                BrushSizeDelete += param;
                if (BrushSizeDelete > 10f) BrushSizeDelete = 10f;
                if (BrushSizeDelete < 1f) BrushSizeDelete = 1f;
                SizeBrushDelet(BrushSizeDelete);

            }
            if (EventLD.EnumModeLD == EnumModeLD.Objects)
            {
                BrashMassSizeWidth += param;
                if (BrashMassSizeWidth > 100f) BrashMassSizeWidth = 100f;
                if (BrashMassSizeWidth < 10f) BrashMassSizeWidth = 10f;
                BrashMassSizeWidthParam(BrashMassSizeWidth);
            }
            if (EventLD.EnumModeLD == EnumModeLD.LandscapeHeight)
            {
                LandscapeHeigntBrushSize += param;
                if (LandscapeHeigntBrushSize > 200f) LandscapeHeigntBrushSize = 200f;
                if (LandscapeHeigntBrushSize < 10f) LandscapeHeigntBrushSize = 10f;
                LandscapeHeigntBrushSizeParam(LandscapeHeigntBrushSize);
            }
            if (EventLD.EnumModeLD == EnumModeLD.BrashSmoothing)
            {
                LandscapeHeigntBrushSize += param;
                if (LandscapeHeigntBrushSize > 100f) LandscapeHeigntBrushSize = 100f;
                if (LandscapeHeigntBrushSize < 10f) LandscapeHeigntBrushSize = 10f;
                LandscapeHeigntBrushSizeParam(LandscapeHeigntBrushSize);
            }
            if (EventLD.EnumModeLD == EnumModeLD.BrashSetHeight)
            {
                LandscapeHeigntBrushSize += param;
                if (LandscapeHeigntBrushSize > 100f) LandscapeHeigntBrushSize = 100f;
                if (LandscapeHeigntBrushSize < 10f) LandscapeHeigntBrushSize = 10f;
                LandscapeHeigntBrushSizeParam(LandscapeHeigntBrushSize);
            }
            if (EventLD.EnumModeLD == EnumModeLD.BrashInclined)
            {
                LandscapeHeigntBrushSize += param;
                if (LandscapeHeigntBrushSize > 100f) LandscapeHeigntBrushSize = 100f;
                if (LandscapeHeigntBrushSize < 10f) LandscapeHeigntBrushSize = 10f;
                LandscapeHeigntBrushSizeParam(LandscapeHeigntBrushSize);
            }

            LeftShiftWheelEvent?.Invoke(param);
        }
        //LeftContro+ колесо
        public static void LeftControlWheel(float param)
        {
            if (EventLD.EnumModeLD == EnumModeLD.LandscapeTexture)
            {
                BrashTexturesForce += param / 50;
                if (BrashTexturesForce < 0f) BrashTexturesForce = 0f;
                if (BrashTexturesForce > 1f) BrashTexturesForce = 1f;
                BrashTexturesForceParam(BrashTexturesForce);
            }
            if (EventLD.EnumModeLD == EnumModeLD.Objects)
            {
                BrashMassSizeDepth += param;
                if (BrashMassSizeDepth > 100f) BrashMassSizeDepth = 100f;
                if (BrashMassSizeDepth < 10f) BrashMassSizeDepth = 10f;
                BrashMassSizeDepthParam(BrashMassSizeDepth);
            }
            if (EventLD.EnumModeLD == EnumModeLD.LandscapeHeight)
            {
                LandscapeHeigntBrushHeight += param;
                if (LandscapeHeigntBrushHeight > 50f) LandscapeHeigntBrushHeight = 50f;
                if (LandscapeHeigntBrushHeight < -10f) LandscapeHeigntBrushHeight = -10f;
                LandscapeHeigntBrushHeightParam(LandscapeHeigntBrushHeight);
            }
            if (EventLD.EnumModeLD == EnumModeLD.BrashSmoothing)
            {
                LandscapeHeigntBrushSmoothing += param;
                if (LandscapeHeigntBrushSmoothing > 100f) LandscapeHeigntBrushSmoothing = 100f;
                if (LandscapeHeigntBrushSmoothing < 10f) LandscapeHeigntBrushSmoothing = 10f;
                LandscapeHeigntBrushSmoothingParam(LandscapeHeigntBrushSmoothing);
            }
            if (EventLD.EnumModeLD == EnumModeLD.BrashSetHeight)
            {
                LandscapeHeigntBrushSmoothing += param;
                if (LandscapeHeigntBrushSmoothing > 100f) LandscapeHeigntBrushSmoothing = 100f;
                if (LandscapeHeigntBrushSmoothing < 10f) LandscapeHeigntBrushSmoothing = 10f;
                LandscapeHeigntBrushSmoothingParam(LandscapeHeigntBrushSmoothing);
            }
            if (EventLD.EnumModeLD == EnumModeLD.BrashInclined)
            {
                LandscapeHeigntBrushSmoothing += param;
                if (LandscapeHeigntBrushSmoothing > 100f) LandscapeHeigntBrushSmoothing = 100f;
                if (LandscapeHeigntBrushSmoothing < 10f) LandscapeHeigntBrushSmoothing = 10f;
                LandscapeHeigntBrushSmoothingParam(LandscapeHeigntBrushSmoothing);
            }
            LeftControlWheelEvent?.Invoke(param);

        }
        // Вращение кисти
        public static void RotateBrushParam(bool param) { RotateBrushEvent?.Invoke(param); RotateBrush = param; }
        //Включение панели высота камеры
        public static void HeightCameraPanel(bool param) { HeightCameraPanelEvent?.Invoke(param); HeightCameraPanelActiv = param; }
        //высота камеры передается колесиком
        public static void HeightCameraWheelParam(float param) { HeightCameraWheelEvent?.Invoke(param); }
        //Горячие клавиши панель
        public static void ModeText(bool param) { ModeTextEvent?.Invoke(param); ModeTextActiv = param; }
        //размер показывающей кисти
        public static void BrushSize(float param) { BrushSizeEvent?.Invoke(param); }

        //Включение отключение курсора
        public static void CursorActiv(bool param)
        {
            //  Debug.Log("Курсор  "+param);
            UnityEngine.Cursor.visible = param;
            Cursor.lockState = param == true ? CursorLockMode.None : CursorLockMode.Locked;
            FPSExplorer(!param);
            if (!param) PanelsNoActiv();
        }
        //Нажатие мыши
        public static void MouseDown(int param)
        {
            // Debug.Log("Нажата мышь  "+param);
            MouseDownEvent?.Invoke(param);
        }
        //Перегененрация кисти
        public static void RandomizeRun() { RandomizeRunEvent?.Invoke(); }
        //Активация Рандома
        public static void RandomizeActiv(bool param) { RandomizeActivEvent?.Invoke(param); Randomize = param; }
        //Активация вертикали
        public static void VerticalActiv(bool param) { VerticalActivEvent?.Invoke(param); Vertical = param; }
        //Активация магнита
        public static void MagnetActiv(bool param) { MagnetActivEvent?.Invoke(param); Magnet = param; }

        public static void NameObjectGrup(string param) { NameObjectGrupEvent?.Invoke(param); }
        public static void SaveLD() { SaveLDEvent?.Invoke(); }
        public static void LoadLD() { LoadLDEvent?.Invoke(); }
        public static void FPSExplorer(bool param) { FPSExplorerActivEvent?.Invoke(param); }
        public static void EnumModeLDParam(EnumModeLD param)
        {
            EnumModeLD = param;
            // Debug.Log(param.ToString());
            EnumModeLDEvent?.Invoke(param);

        }
        #endregion

    }
}
