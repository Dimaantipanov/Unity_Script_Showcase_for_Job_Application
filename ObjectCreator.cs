using cakeslice;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class ObjectCreator : MonoBehaviour
    {
        [HideInInspector]
        public PrefabLD[] ArrayObject;//Сюда вставить объекты которые расставляем
        [HideInInspector]
        public Transform Goal;
        [HideInInspector]
        public Transform ObjectContainer;
        [HideInInspector]
        public Transform Camera;
        [HideInInspector]
        public List<GameObject> RockObjectsSave = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> StoneObjectsSave = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> TreesObjectsSave = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> BushesObjectsSave = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> GrassObjectsSave = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> HousesObjectsSave = new List<GameObject>();
        public GameObject Terrain_LD;
      
        public Mesh TerrainLDMesh;
        [HideInInspector]
        public string ObjectGroupName;
        [HideInInspector]
        public float BrushWidth;//Ширина кисти
        [HideInInspector]
        public float BrushDepth;//Глубина кисти
        [HideInInspector]
        public int NumberOfObjects;//Количество генер объектов
        [HideInInspector]
        public int NumListObject;
        [HideInInspector]
        public bool Randomize = false;
        [HideInInspector]
        public float DistanseDelete;
        [HideInInspector]
        public bool DeleteActiv;

        private List<GameObject> BrashList;
        private RaycastHit Hit;
        private ObjectInitialization ObjectInitialization;
        private ObjectContainerScripts ObjectContainerScripts;
        private Vector3 OldPositionContainer;
        private List<Vector3> BrashListPosition;
        private List<Vector3> BrashListRotation;

        #region инициализация
        private void Awake()
        {
            ObjectInitialization = gameObject.GetComponent<ObjectInitialization>();
            Initialization();
            
        }
        public void Initialization()
        {
            ObjectInitialization.Initialization();
            //  Container = new GameObject("map_" + DateTime.Now.ToString("ddMMyyyy_HHmmss"));
            Goal.position = Camera.position + new Vector3(0, 0, 5);
            BrashList = new List<GameObject>();
            BrashListPosition = new List<Vector3>();
            BrashListRotation = new List<Vector3>();
            ObjectContainer.localScale = Vector3.one; //reset zoom
            ObjectContainer.localEulerAngles = Vector3.zero;   //reset rotation
            ObjectContainerScripts = ObjectContainer.GetComponent<ObjectContainerScripts>();
            EventLD.MagnetActiv(true);

        }
        private void Start()
        {
            InActivOutlineNew();
            EventLD.ObjectCreator = this;
            EventLD.OnObjectCreatorLoading();
        }

        #endregion
        #region Ивенты
        private void OnEnable()
        {
            EventLD.KeyboardUpEvent += KeyboardUp;
            EventLD.RandomizeActivEvent += RandomizeActiv;
            EventLD.RandomizeRunEvent += RandomBrash;
            EventLD.EnumModeLDEvent += EnumModeLDParam;
            EventLD.MouseDownEvent += MouseDown;
            EventLD.SizeBrushDeletEvent += SizeBrushDeletParam;
            EventLD.BrashMassSizeWidthEvent += BrashMassSizeWidth;
            EventLD.BrashMassSizeDepthEvent += BrashMassSizeDepth;
            EventLD.BrashMassNumberOfObjectsEvent += BrashMassNumberOfObjects;
            EventLD.SaveLDEvent += ConversionObject;


        }
        private void OnDisable()
        {
            EventLD.SaveLDEvent -= ConversionObject;
            EventLD.KeyboardUpEvent -= KeyboardUp;
            EventLD.RandomizeActivEvent -= RandomizeActiv;
            EventLD.RandomizeRunEvent -= RandomBrash;
            EventLD.EnumModeLDEvent -= EnumModeLDParam;
            EventLD.MouseDownEvent -= MouseDown;


            EventLD.SizeBrushDeletEvent -= SizeBrushDeletParam;

            EventLD.BrashMassSizeWidthEvent -= BrashMassSizeWidth;
            EventLD.BrashMassSizeDepthEvent -= BrashMassSizeDepth;
            EventLD.BrashMassNumberOfObjectsEvent -= BrashMassNumberOfObjects;
        }
        private void ConversionObject()
        {
            // gameObject.GetComponent<ConversionObject>().ObjectConversion();
        }

        private void BrashMassNumberOfObjects(float param)
        {
            NumberOfObjects = (int)param;
            if (EventLD.EnumModeLD == EnumModeLD.SettingsObjects || EventLD.EnumModeLD == EnumModeLD.Objects)
            {

                RandomBrash();
            }
        }
        private void BrashMassSizeDepth(float param)
        {
            BrushDepth = param;
            if (EventLD.EnumModeLD == EnumModeLD.SettingsObjects || EventLD.EnumModeLD == EnumModeLD.Objects)
            {

                RandomBrash();
            }

        }
        private void BrashMassSizeWidth(float param)
        {
            BrushWidth = param;
            if (EventLD.EnumModeLD == EnumModeLD.SettingsObjects || EventLD.EnumModeLD == EnumModeLD.Objects)
            {
                RandomBrash();
            }
        }
        private void SizeBrushDeletParam(float param)
        {
            DistanseDelete = param;
        }
        private void KeyboardUp(int param)
        {
            if (EventLD.EnumModeLD != EnumModeLD.Object) return;
            if (param == 1) SlotSelect(true);
            if (param == 2) SlotSelect(false);
        }


        private void MouseDown(int param)
        {
            switch (EventLD.EnumModeLD)
            {
                case EnumModeLD.none:
                    break;
                case EnumModeLD.Settings:

                    break;
                case EnumModeLD.SettingsObjects:
                    if (param == 0) RandomBrash();
                    break;
                case EnumModeLD.LandscapeHeight:
                    break;

                case EnumModeLD.LandscapeTexture:
                    break;

                case EnumModeLD.Object:
                    if (param == 0) { SetObject(); RandomizeActiv(true); }
                    break;
                case EnumModeLD.Objects:
                    if (param == 0) { SetObject(); RandomizeActiv(true); }
                    break;
                case EnumModeLD.Lastic:
                    if (param == 0) DeleteActiv = true;
                    break;
                case EnumModeLD.Interface:
                    break;
                default:
                    break;
            }
        }
        private void EnumModeLDParam(EnumModeLD param)
        {
            switch (param)
            {
                case EnumModeLD.none:
                    DelObject(true);
                    break;
                case EnumModeLD.Settings:
                    break;
                case EnumModeLD.SettingsHeight:
                    break;
                case EnumModeLD.SettingsObjects:
                    break;
                case EnumModeLD.SettingsLandscapeHeight:
                    break;
                case EnumModeLD.SettingTextures:
                    break;
                case EnumModeLD.SettingEnvironment:
                    DelObject(true);
                    break;
                case EnumModeLD.SettingBrashSmoothing:
                    break;
                case EnumModeLD.SettingBrashSetHeight:
                    break;
                case EnumModeLD.SettingBrashInclined:
                    break;
                case EnumModeLD.SettingGeneratorMap:
                    break;
                case EnumModeLD.LandscapeHeight:
                    DelObject(true);
                    break;
                case EnumModeLD.LandscapeTexture:
                    DelObject(true);
                    break;
                case EnumModeLD.BrashSmoothing:
                    DelObject(true);
                    break;
                case EnumModeLD.BrashSetHeight:
                    DelObject(true);
                    break;
                case EnumModeLD.BrashInclined:
                    DelObject(true);
                    break;
                case EnumModeLD.Object:
                    EventLD.BrushSize(0);
                    RandomBrash();
                    break;
                case EnumModeLD.Objects:
                    EventLD.BrushSize(0);
                    RandomBrash();
                    break;
                case EnumModeLD.Lastic:
                    DeleteActiv = true;
                    DelObject(true);
                    EventLD.BrushSize(DistanseDelete * 2);
                    break;
                case EnumModeLD.GeneratorMap:
                    DelObject(true);
                    break;
                case EnumModeLD.Interface:
                    DelObject(true);
                    break;
                case EnumModeLD.PanelMode:
                    DelObject(true);
                    break;
                default:
                    break;
            }

            if (param != EnumModeLD.Lastic) NoLastic();
        }
        private void NoLastic()
        {

            DeleteActiv = false;
            DelObject(false);
            NoActivOutline();
        }
        //При выходе из режима ластка
        private void NoActivOutline()
        {
            NoActiv(RockObjectsSave);
            NoActiv(StoneObjectsSave);
            NoActiv(TreesObjectsSave);
            NoActiv(BushesObjectsSave);
            NoActiv(GrassObjectsSave);
            NoActiv(HousesObjectsSave);
        }
        private void NoActiv(List<GameObject> param)
        {
            for (int i = 0; i < param.Count; i++)
            {
                param[i].GetComponent<OutlineNew>().enabled = false;
            }
        }
        private void RandomizeActiv(bool param)
        {
            if (EventLD.Randomize)
            {

                RandomBrash();
            }
        }
        #endregion



        #region UpDate
        void Update()
        {
            BrashListPositionRotation();
            if (EventLD.EnumModeLD == EnumModeLD.Object) BrashMagnet();
            if (EventLD.EnumModeLD == EnumModeLD.Objects) BrashMagnet();
            if (EventLD.EnumModeLD == EnumModeLD.Lastic) DeleteObject(DeleteActiv);

        }
        #endregion


        #region Изменение превью слота

        public void RandomBrash()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
            //  Debug.Log("RandomBrash");
            BrashList = new List<GameObject>();
            BrashListPosition = new List<Vector3>();
            BrashListRotation = new List<Vector3>();
            ObjectContainerScripts.DeleteList();
            int temp = 0;

            temp = EventLD.EnumModeLD == EnumModeLD.Objects ? NumberOfObjects : 1;
            if (EventLD.EnumModeLD == EnumModeLD.SettingsObjects) temp = NumberOfObjects;
            // Debug.Log(EventLD.EnumModeLD);
            for (int i = 0; i < temp; i++)
            {
                int numGameObject = 0;

                if (EventLD.EnumModeLD == EnumModeLD.SettingsObjects) numGameObject = RandomLD.RandomInt(0, ArrayObject.Length);
                if (EventLD.EnumModeLD == EnumModeLD.Objects) numGameObject = RandomLD.RandomInt(0, ArrayObject.Length);
                if (EventLD.EnumModeLD == EnumModeLD.Object) numGameObject = NumListObject;
                if (ArrayObject.Length == 0) return;
                GameObject gameObjectTemp = ArrayObject[numGameObject].Prefab;
                ObjectLD objectLD = ArrayObject[numGameObject].Prefab.GetComponent<ObjectLD>();
                // Vertical = objectLD.Vertical;
                EventLD.VerticalActiv(objectLD.Vertical);
                Vector3 position = new Vector3();
                if (EventLD.EnumModeLD == EnumModeLD.Objects) position = new Vector3(RandomLD.RandomFloat(-BrushWidth, BrushWidth), 5, RandomLD.RandomFloat(0, BrushDepth * 2));
                if (EventLD.EnumModeLD == EnumModeLD.SettingsObjects) position = new Vector3(RandomLD.RandomFloat(-BrushWidth, BrushWidth), 5, RandomLD.RandomFloat(0, BrushDepth * 2));
                if (EventLD.EnumModeLD == EnumModeLD.Object) position = new Vector3(0, 0, 0);

                GameObject gameObjectT = (GameObject)Instantiate(gameObjectTemp, ObjectContainer.position + position, ObjectContainer.rotation);
                gameObjectT.transform.localScale = gameObjectT.transform.localScale * (RandomLD.RandomScale(objectLD.ScaleFactor));
                if (objectLD.Vertical)
                {
                    gameObjectT.transform.localEulerAngles = RandomLD.RandomRotationTree();

                }
                else
                {
                    gameObjectT.transform.localEulerAngles = RandomLD.RandomRotationTree();

                }

                //  gameObjectT.transform.SetParent(ObjectContainer);
                BrashList.Add(gameObjectT);
                BrashListRotation.Add(gameObjectT.transform.eulerAngles);
                BrashListPosition.Add(gameObjectT.transform.position - ObjectContainer.transform.position);
                ObjectContainerScripts.ObjectContainerList.Add(gameObjectT);
            }
            OldPositionContainer = ObjectContainer.transform.position;
            if (!EventLD.Magnet) BrashMagnet();
            EventLD.NameObjectGrup(ObjectGroupName);
        }
        #endregion
        private Vector3 RotateY(Vector3 postition, double angleRad)
        {
            return new Vector3((float)(postition.x * Math.Cos(angleRad) - postition.z * Math.Sin(angleRad)),
                postition.y,
                (float)(postition.x * Math.Sin(angleRad) + postition.z * Math.Cos(angleRad)));
        }

        private void BrashListPositionRotation()
        {

            if (EventLD.EnumModeLD == EnumModeLD.Interface) return;
            if (EventLD.EnumModeLD == EnumModeLD.SettingEnvironment) return;
            if (EventLD.EnumModeLD == EnumModeLD.Settings) return;
            if (EventLD.EnumModeLD == EnumModeLD.SettingsHeight) return;
            if (EventLD.EnumModeLD == EnumModeLD.SettingsLandscapeHeight) return;
            if (EventLD.EnumModeLD == EnumModeLD.SettingsObjects) return;
            if (EventLD.EnumModeLD == EnumModeLD.SettingTextures) return;
            if (EventLD.EnumModeLD == EnumModeLD.LandscapeHeight) return;
            if (EventLD.EnumModeLD == EnumModeLD.LandscapeTexture) return;

            if (EventLD.EnumModeLD == EnumModeLD.none) return;
            if (BrashList.Count == 0) return;
            var newPositonConteiner = ObjectContainer.transform.position;
            var angle = ObjectContainer.eulerAngles.y / 180 * Mathf.PI;
            for (int i = 0; i < BrashList.Count; i++)
            {
                var position = BrashListPosition[i];
                position = RotateY(position, angle) + newPositonConteiner;
                BrashList[i].transform.position = position;
            }
            OldPositionContainer = newPositonConteiner;
        }

        #region Изменение слота в массиве
        public void SlotSelect(bool param)
        {
            if (param)
            {
                NumListObject++;
                if (NumListObject == ArrayObject.Length) NumListObject = 0;
            }
            else
            {
                NumListObject--;
                if (NumListObject < 0) NumListObject = ArrayObject.Length - 1;
            }
            EventLD.VerticalActiv(ArrayObject[NumListObject].Prefab.GetComponent<ObjectLD>().Vertical);
            RandomBrash();
        }
        #endregion




        #region Добавление
        public void SetObject()
        {

            for (int i = 0; i < BrashList.Count; i++)
            {
                SetObjectDop(BrashList[i]);
            }
            if (Randomize)
            {
                // Debug.Log("Тута");
                RandomBrash();
            }

        }
        private void SetObjectDop(GameObject ObjectList)
        {
            if (!ObjectList) return;

            GameObject clone = (GameObject)Instantiate(ObjectList, ObjectList.transform.position, ObjectList.transform.rotation);
            clone.transform.localScale = Vector3.Scale(ObjectList.transform.localScale, ObjectContainer.localScale);
            clone.name = ObjectList.name;
            ObjectLD TempObjectLD = clone.GetComponent<ObjectLD>();
            TempObjectLD.InActivColliders(true);
            TempObjectLD.ObjectLDPosition = clone.transform.position;
            TempObjectLD.ObjectLDRotation = clone.transform.rotation;
            TempObjectLD.ObjectLDScale = clone.transform.localScale;
            SortingIntoAnArray(clone);

        }
        //Сортировка в массивы

        #endregion
        #region Удаление объектов с кисти

        public void DelObject(bool param)
        {
            if (param)
            {
                BrashList = new List<GameObject>();
                BrashListPosition = new List<Vector3>();
                BrashListRotation = new List<Vector3>();
                ObjectContainerScripts.DeleteList();
            }


        }
        public void InActivOutlineNew()
        {
            //   Debug.Log("Не активный OutlineNew ");
            InActiv(RockObjectsSave);
            InActiv(StoneObjectsSave);
            InActiv(TreesObjectsSave);
            InActiv(BushesObjectsSave);
            InActiv(GrassObjectsSave);
            InActiv(HousesObjectsSave);
            for (int i = 0; i < ArrayObject.Length; i++)
            {
                ArrayObject[i].Prefab.GetComponent<OutlineNew>().enabled = true;
                ArrayObject[i].Prefab.GetComponent<OutlineNew>().enabled = false;
            }
            ObjectContainerScripts.InActivOutlineNew();
            void InActiv(List<GameObject> List)
            {
                if (List.Count == 0) return;
                for (int i = 0; i < List.Count; i++)
                {
                    List[i].gameObject.GetComponent<OutlineNew>().enabled = true;
                    List[i].gameObject.GetComponent<OutlineNew>().enabled = false;
                }
            }
        }
        private void DeleteObject(bool Act)
        {
            // Debug.Log("Активирована кисть удаления");
            DeleteListObgect(RockObjectsSave, Act);
            DeleteListObgect(StoneObjectsSave, Act);
            DeleteListObgect(TreesObjectsSave, Act);
            DeleteListObgect(BushesObjectsSave, Act);
            DeleteListObgect(GrassObjectsSave, Act);
            DeleteListObgect(HousesObjectsSave, Act);
            DeleteActiv = false;
        }
        private void DeleteListObgect(List<GameObject> List, bool param)
        {
            Vector3 PositionGoal = Goal.transform.position;
            Vector3 PositionListObject = new Vector3();
            for (int i = 0; i < List.Count; i++)
            {
                PositionListObject = List[i].gameObject.transform.position;
                float Distanse = Vector3.Distance(PositionGoal, PositionListObject);
                if (DistanseDelete > Distanse)
                {
                    List[i].gameObject.GetComponent<OutlineNew>().enabled = true;
                    if (param)
                    {
                        Destroy(List[i]);
                        List.RemoveAt(i);
                    }
                }
                else
                {
                    List[i].gameObject.GetComponent<OutlineNew>().enabled = false;
                }
            }

        }

        #endregion
        #region Сортировка в массивы
        private void SortingIntoAnArray(GameObject Obj)
        {
            switch (Obj.GetComponent<ObjectLD>().ObjectTypeLD)
            {
                case ObjectTypeLD.Rock:
                    RockObjectsSave.Add(Obj);
                    break;
                case ObjectTypeLD.Stone:
                    StoneObjectsSave.Add(Obj);
                    break;
                case ObjectTypeLD.Trees:
                    TreesObjectsSave.Add(Obj);
                    break;
                case ObjectTypeLD.Bushes:
                    BushesObjectsSave.Add(Obj);
                    break;
                case ObjectTypeLD.Grass:
                    GrassObjectsSave.Add(Obj);
                    break;
                case ObjectTypeLD.Houses:
                    HousesObjectsSave.Add(Obj);
                    break;

            }
        }
        #endregion

        #region Магнит к земле
        private void BrashMagnet()
        {
            if (BrashList.Count == 0) return;
            for (int i = 0; i < BrashList.Count; i++)
            {
                Vector3 Direction = Vector3.down;
                Vector3 a = new Vector3(BrashList[i].gameObject.transform.position.x, BrashList[i].gameObject.transform.position.y + 50, BrashList[i].gameObject.transform.position.z);
                if (Physics.Raycast(a, Direction, out Hit, 500f))
                {
                    if (Hit.collider)
                    {

                        if (BrashList[i].gameObject.transform.GetComponent<ObjectLD>())
                        {
                            var b = new Vector3();
                            if (Hit.collider.gameObject.GetComponent<CapsuleCollider>())
                            {
                                b = new Vector3(Hit.point.x, Hit.point.y, Hit.point.z + 0.5f);
                            }
                            else { b = new Vector3(Hit.point.x, Hit.point.y, Hit.point.z); }
                       

                            BrashList[i].gameObject.transform.position = b;
                            if (BrashList[i].gameObject.transform.GetComponent<ObjectLD>().Vertical)
                            {
                                BrashList[i].gameObject.transform.localEulerAngles = new Vector3(0, BrashList[i].gameObject.transform.rotation.y, 0);
                            }
                            else
                            {
                                var angle = BrashListRotation[i];
                                Quaternion HitRotation = Quaternion.FromToRotation(Vector3.up, Hit.normal);
                                BrashList[i].gameObject.transform.SetPositionAndRotation(Hit.point, HitRotation);
                                BrashList[i].gameObject.transform.RotateAroundLocal(Hit.normal, angle.y / 180 * Mathf.PI);
                            }
                        }
                    }
                }
            }
        }
        #endregion

       
    }
}

