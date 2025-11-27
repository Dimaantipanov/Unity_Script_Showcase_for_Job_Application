// Изготовитель скрипта: Антипанов Дмитрий
// Назначение: Обработка нажатий клавиш и вызов соответствующих событий для управления режимами редактора и параметрами среды.

using UnityEngine;

namespace LD
{
    public class KeyboardPress : MonoBehaviour
    {
        // Инициализация состояния режима при запуске
        private void Awake()
        {
            EventLD.EnumModeLDParam(EnumModeLD.none);
        }

        // Обработка ввода каждый кадр
        void Update()
        {
            PressingKeyKeyboard();
            PressingKeyKeyboardMode();
        }

        #region Нажатие клавиш

        // Обработка нажатий основных клавиш и колесика мыши
        private void PressingKeyKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.T)) EventLD.MagnetActiv(!EventLD.Magnet);
            if (Input.GetKeyDown(KeyCode.V)) EventLD.VerticalActiv(!EventLD.Vertical);
            if (Input.GetKeyDown(KeyCode.Q)) EventLD.RandomizeActiv(!EventLD.Randomize);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (EventLD.EnumModeLD == EnumModeLD.Object || EventLD.EnumModeLD == EnumModeLD.Object)
                {
                    EventLD.RandomizeRun();
                }
            }
            if (Input.GetKey(KeyCode.R)) EventLD.RotateBrushParam(true);
            if (EventLD.RotateBrush && !Input.GetKey(KeyCode.R)) EventLD.RotateBrushParam(false);
            if (Input.GetKeyUp(KeyCode.F1)) EventLD.ModeText(!EventLD.ModeTextActiv);
            if (Input.GetKeyUp(KeyCode.H)) EventLD.HeightCameraPanel(!EventLD.HeightCameraPanelActiv);
            if (Input.GetKeyUp(KeyCode.BackQuote)) EventLD.EnvironmentParam(!EventLD.Environment);

            if (Input.GetKeyDown(KeyCode.Mouse0)) { EventLD.MouseDown(0); }
            if (Input.GetKeyDown(KeyCode.Mouse1)) { EventLD.MouseDown(1); }
            if (Input.GetKeyUp(KeyCode.Alpha1)) EventLD.KeyboardUp(1);
            if (Input.GetKeyUp(KeyCode.Alpha2)) EventLD.KeyboardUp(2);
            if (Input.GetKeyUp(KeyCode.Alpha3)) EventLD.KeyboardUp(3);
            if (Input.GetKeyUp(KeyCode.Alpha4)) EventLD.KeyboardUp(4);

            // Обработка прокрутки колесика мыши для разных модификаторов
            HeightBrushWheel();

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.mouseScrollDelta.y != 0)
                    EventLD.LeftControlWheel(Input.mouseScrollDelta.y);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.mouseScrollDelta.y != 0)
                    EventLD.LeftShiftWheel(Input.mouseScrollDelta.y);
            }
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.mouseScrollDelta.y != 0)
                    EventLD.LeftAltWheel(Input.mouseScrollDelta.y);
            }
        }

        // Обработка прокрутки колесика мыши без модификаторов
        private void HeightBrushWheel()
        {
            if (Input.GetKey(KeyCode.LeftControl)) return;
            if (Input.GetKey(KeyCode.LeftShift)) return;
            if (Input.GetKey(KeyCode.LeftAlt)) return;
            if (Input.mouseScrollDelta.y != 0)
                EventLD.HeightCameraWheelParam(Input.mouseScrollDelta.y * 5);
        }

        // Обработка нажатий клавиш, отвечающих за смену режимов и сохранение/загрузку
        private void PressingKeyKeyboardMode()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                EventLD.SaveLD();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                EventLD.LoadLD();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                var a = EventLD.EnumModeLD != EnumModeLD.Objects ? EnumModeLD.Objects : EnumModeLD.Object;
                EventLD.EnumModeLDParam(a);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                EventLD.EnumModeLDParam(EventLD.EnumModeLD == EnumModeLD.Lastic ? EnumModeLD.none : EnumModeLD.Lastic);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                EventLD.EnumModeLDParam(EventLD.EnumModeLD == EnumModeLD.LandscapeHeight ? EnumModeLD.none : EnumModeLD.LandscapeHeight);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                EventLD.EnumModeLDParam(EventLD.EnumModeLD == EnumModeLD.LandscapeTexture ? EnumModeLD.none : EnumModeLD.LandscapeTexture);
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                EventLD.ActivPanelMode();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EventLD.EnumModeLDParam(EnumModeLD.ExitMenu);
            }
        }

        #endregion
    }
}
