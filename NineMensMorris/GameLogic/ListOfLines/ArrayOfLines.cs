using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NineMensMorris.Models;

namespace NineMensMorris.GameLogic
{
    internal static partial class CollectionOfLines
    {
        public static readonly Dictionary<Models.ButtonPosition, Models.ButtonPosition[][]> MatchingLinesForTheButton;
        static CollectionOfLines()
        {
            MatchingLinesForTheButton = new(Models.GameState.QuantityOfTheButtons);
            #region SetLines
            #region A
            MatchingLinesForTheButton[key: ButtonPosition.a1] = new ButtonPosition[2][]
                {
                    Columns.First,
                    Rows.A
                };
            MatchingLinesForTheButton[key: ButtonPosition.a4] = new ButtonPosition[2][]
               {
                    Columns.Fourth_Left,
                    Rows.A
               };
            MatchingLinesForTheButton[key: ButtonPosition.a7] = new ButtonPosition[2][]
               {
                    Columns.Seventh,
                    Rows.A
               };
            #endregion A
            #region B
            MatchingLinesForTheButton[key: ButtonPosition.b2] = new ButtonPosition[2][]
                {
                    Columns.Second,
                    Rows.B
                };
            MatchingLinesForTheButton[key: ButtonPosition.b4] = new ButtonPosition[2][]
               {
                    Columns.Fourth_Left,
                    Rows.B
               };
            MatchingLinesForTheButton[key: ButtonPosition.b6] = new ButtonPosition[2][]
               {
                    Columns.Sixth,
                    Rows.B
               };
            #endregion B
            #region C
            MatchingLinesForTheButton[key: ButtonPosition.c4] = new ButtonPosition[2][]
               {
                    Columns.Fourth_Left,
                    Rows.C
               };
            MatchingLinesForTheButton[key: ButtonPosition.c5] = new ButtonPosition[2][]
               {
                    Columns.Fifth,
                    Rows.C
               };
            MatchingLinesForTheButton[key: ButtonPosition.c3] = new ButtonPosition[2][]
               {
                    Columns.Third,
                    Rows.C
               };
            #endregion C
            #region D
            MatchingLinesForTheButton[key: ButtonPosition.d1] = new ButtonPosition[2][]
               {
                    Columns.First,
                    Rows.D_Upper
               };
            MatchingLinesForTheButton[key: ButtonPosition.d2] = new ButtonPosition[2][]
               {
                    Columns.Second,
                    Rows.D_Upper
               };
            MatchingLinesForTheButton[key: ButtonPosition.d3] = new ButtonPosition[2][]
               {
                    Columns.Third,
                    Rows.D_Upper
               };

            MatchingLinesForTheButton[key: ButtonPosition.d5] = new ButtonPosition[2][]
               {
                    Columns.Fifth,
                    Rows.D_Lower
               };
            MatchingLinesForTheButton[key: ButtonPosition.d6] = new ButtonPosition[2][]
               {
                    Columns.Sixth,
                    Rows.D_Lower
               };
            MatchingLinesForTheButton[key: ButtonPosition.d7] = new ButtonPosition[2][]
               {
                    Columns.Seventh,
                    Rows.D_Lower
               };
            #endregion D
            #region E
            MatchingLinesForTheButton[key: ButtonPosition.e3] = new ButtonPosition[2][]
               {
                    Columns.Third,
                    Rows.E
               };
            MatchingLinesForTheButton[key: ButtonPosition.e4] = new ButtonPosition[2][]
               {
                    Columns.Fourth_Right,
                    Rows.E
               };
            MatchingLinesForTheButton[key: ButtonPosition.e5] = new ButtonPosition[2][]
               {
                    Columns.Fifth,
                    Rows.E
               };
            #endregion E
            #region F
            MatchingLinesForTheButton[key: ButtonPosition.f2] = new ButtonPosition[2][]
               {
                    Columns.Second,
                    Rows.F
               };
            MatchingLinesForTheButton[key: ButtonPosition.f4] = new ButtonPosition[2][]
               {
                    Columns.Fourth_Right,
                    Rows.F
               };
            MatchingLinesForTheButton[key: ButtonPosition.f6] = new ButtonPosition[2][]
               {
                    Columns.Sixth,
                    Rows.F
               };
            #endregion F
            #region G
            MatchingLinesForTheButton[key: ButtonPosition.g1] = new ButtonPosition[2][]
               {
                    Columns.First,
                    Rows.G
               };
            MatchingLinesForTheButton[key: ButtonPosition.g4] = new ButtonPosition[2][]
               {
                    Columns.Fourth_Right,
                    Rows.G
               };
            MatchingLinesForTheButton[key: ButtonPosition.g7] = new ButtonPosition[2][]
               {
                    Columns.Seventh,
                    Rows.G
               };
            #endregion G
            #endregion SetLines
        }
        public static List<List<ButtonPosition>> GetListOfNeighbours(in ButtonPosition buttonPosition)
        {
            byte positionInArray = 0;
            List<List<ButtonPosition>> list = new List<List<ButtonPosition>>(2);
            var possitions = MatchingLinesForTheButton[buttonPosition];
            foreach (var array in possitions)
            {
                var smallList = new List<ButtonPosition>();
                foreach(var pos in array)
                {
                    if (buttonPosition == pos)
                    {
                        switch(positionInArray)
                        {
                            case 0:
                                smallList.Add(array[positionInArray+1]);
                                break;
                            case 1:
                                smallList.Add(array[positionInArray+1]);
                                smallList.Add(array[positionInArray-1]);
                                break;
                            case 2:
                                smallList.Add(array[positionInArray-1]);
                                break;
                            default:
                                throw new NotImplementedException($"Too much neighbour buttons in {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                        }
                        positionInArray = 0;
                        list.Add(smallList);
                        break;
                    }
                    positionInArray++;
                }
            }
            return list;

        }
    }
}
