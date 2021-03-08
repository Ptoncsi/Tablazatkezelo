using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            string[] csv_sorok = File.ReadAllText("./csv.csv").Split("\r\n");
            List<List<string>> csv_cells = new List<List<string>>();

            //Console.WriteLine(csv_sorok.Length);
            for (int i = 0; i < csv_sorok.Length; i++)
            {
                csv_cells.Add(new List<string>());
                for (int j = 0; j < csv_sorok[i].Split(";").Length; j++)
                {
                    csv_cells[i].Add(csv_sorok[i].Split(";")[j]);
                }
            }
            csv_cells = addCell(csv_cells,5,5,"hala");
            //Console.WriteLine(csv_cells[5][5]);
            csv_cells = addCell(csv_cells, 7, 7, "a");
            int printtable_result_x, printtable_result_y;
            List<List<string>> printtable_result_table;
            int[] balfelso;
            (printtable_result_x, printtable_result_y, printtable_result_table, balfelso) = printTable(1, 1, csv_cells, "up", new int[] { 1, 1 });
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        //Console.WriteLine(csv_cells.Count);
                        (printtable_result_x, printtable_result_y, printtable_result_table,balfelso) = printTable(printtable_result_x,printtable_result_y,printtable_result_table,"u",balfelso);
                        break;
                    case ConsoleKey.DownArrow:
                        //Console.WriteLine(csv_cells.Count);
                        (printtable_result_x, printtable_result_y, printtable_result_table,balfelso) = printTable(printtable_result_x, printtable_result_y, printtable_result_table, "d",balfelso);
                        break;
                    case ConsoleKey.LeftArrow:
                        //Console.WriteLine(csv_cells.Count);
                        (printtable_result_x, printtable_result_y, printtable_result_table,balfelso) = printTable(printtable_result_x, printtable_result_y, printtable_result_table, "l",balfelso);
                        break;
                    case ConsoleKey.RightArrow:
                        //Console.WriteLine(csv_cells.Count);
                        (printtable_result_x, printtable_result_y, printtable_result_table,balfelso) = printTable(printtable_result_x, printtable_result_y, printtable_result_table, "r",balfelso);
                        break;
                    case ConsoleKey.Delete:
                        printtable_result_table[printtable_result_x - 1][printtable_result_y - 1] = "";
                        (printtable_result_x, printtable_result_y, printtable_result_table, balfelso) = printTable(printtable_result_x, printtable_result_y, printtable_result_table, "", balfelso);
                        break;
                    case ConsoleKey.Home:
                        (printtable_result_x, printtable_result_y, printtable_result_table, balfelso) = printTable(1, 1, printtable_result_table, "", new int[] { 1, 1 });
                        break;
                    case ConsoleKey.Backspace:
                        if (printtable_result_table[printtable_result_x - 1][printtable_result_y - 1].Length > 0)
                        {
                            printtable_result_table[printtable_result_x - 1][printtable_result_y - 1] = printtable_result_table[printtable_result_x - 1][printtable_result_y - 1].Substring(0, printtable_result_table[printtable_result_x - 1][printtable_result_y - 1].Length - 1);
                            (printtable_result_x, printtable_result_y, printtable_result_table, balfelso) = printTable(printtable_result_x, printtable_result_y, printtable_result_table, "", balfelso);
                        }
                        break;
                    default:
                        if (!(((key.Modifiers & ConsoleModifiers.Alt) != 0) | ((key.Modifiers & ConsoleModifiers.Control) != 0))) {
                            if (Regex.IsMatch(key.KeyChar.ToString(), @"^[a-zA-Z0-9]+$")) {
                                printtable_result_table[printtable_result_x - 1][printtable_result_y - 1] += key.KeyChar.ToString();
                                (printtable_result_x, printtable_result_y, printtable_result_table, balfelso) = printTable(printtable_result_x, printtable_result_y, printtable_result_table, "", balfelso);
                            }
                        } else
                        {
                            if (((key.Modifiers & ConsoleModifiers.Control) != 0)&(key.Key.ToString()=="S"))
                            {
                                string save_out = "";
                                List<string> save_out_arr = new List<string>();
                                for (int i = 0; i < printtable_result_table.Count; i++)
                                {
                                    save_out_arr.Add(String.Join(';',printtable_result_table[i]));
                                }
                                save_out = String.Join("\r\n",save_out_arr);
                                File.WriteAllText("./csv.csv", save_out, Encoding.UTF8);
                                Console.WriteLine("Saved!");
                            }
                        }
                        break;

                }
            }
        }


        static (int x,int y, List<List<string>> table, int[] balfelso) printTable(int x,int y, List<List<string>> table, string direction, int[] balfelso)
        {
            #region nav
            switch (direction)
            {
                case "u":
                    if (!(x == 1))
                    {
                        if (x < 4)
                        {
                            x--;
                        }
                        else
                        {
                            if (x-balfelso[0]>2)
                            {
                                x--;
                            }
                            else
                            {
                                balfelso[0]--;
                                x--;
                            }
                        }
                    }
                    break;
                case "d":
                    if (!(x == int.MaxValue))
                    {
                        if (x > int.MaxValue-3)
                        {
                            x++;
                        }
                        else
                        {
                            if (x-balfelso[0] < 4)
                            {
                                x++;
                            }
                            else
                            {
                                table = addCell(table, balfelso[0] + 7, balfelso[1] + 7, "");
                                balfelso[0]++;
                                x++;
                            }
                        }
                    }
                    break;
                case "l":
                    if (!(y == 1))
                    {
                        if (y < 4)
                        {
                            y--;
                        }
                        else
                        {
                            if (y - balfelso[1] > 2)
                            {
                                y--;
                            }
                            else
                            {
                                balfelso[1]--;
                                y--;
                            }
                        }
                    }
                    break;
                case "r":
                    if (!(y == int.MaxValue))
                    {
                        if (y > int.MaxValue - 3)
                        {
                            y++;
                        }
                        else
                        {
                            if (y - balfelso[1] < 4)
                            {
                                y++;
                            }
                            else
                            {
                                table = addCell(table, balfelso[0] + 7, balfelso[1] + 7, "");
                                balfelso[1]++;
                                y++;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion

            string[,] template = new string[17,17];
            #region template_allitas
            #region template_0
            template[0,0] = " ";
            template[0,1] = "            ";
            template[0,2] = "┌";
            template[0,3] = "────────────";
            template[0, 4] = "┬";
            template[0, 5] = "────────────";
            template[0, 6] = "┬";
            template[0, 7] = "────────────";
            template[0, 8] = "┬";
            template[0, 9] = "────────────";
            template[0, 10] = "┬";
            template[0, 11] = "────────────";
            template[0, 12] = "┬";
            template[0, 13] = "────────────";
            template[0, 14] = "┬";
            template[0, 15] = "────────────";
            template[0, 16] = "┐";
            #endregion
            #region template_1
            template[1, 0] = " ";
            template[1, 1] = "            ";
            template[1, 2] = "│";
            for (int i = 0; i < 7; i++)
            {
                template[1, i * 2 + 3] = stringLimit((balfelso[1] + i).ToString(), 12);
                template[1, i * 2 + 4] = "│";
            }
            #endregion
            #region template_2
            template[2, 0] = "┌";
            template[2, 1] = "────────────";
            for (int i = 0; i < 7; i++)
            {
                template[2, i * 2 + 2] = "┼";
                template[2, i * 2 + 3] = "────────────";
            }
            template[2, 16] = "┤";
            #endregion
            #region tbody
            for (int i = 0; i < 7; i++)
            {
                template[i * 2 + 3, 0] = "│";
                template[i * 2 + 3, 1] = stringLimit((balfelso[0] + i).ToString(), 12);
                for (int j = 0; j < 7; j++)
                {
                    template[i * 2+3, j * 2 + 2] = "│";
                    template[i * 2 + 3, j * 2 + 3] = stringLimit(table[balfelso[0] - 1 + i][balfelso[1] - 1 + j], 12);
                    template[i * 2 + 4, j * 2 + 2] = "┼";
                    template[i * 2 + 4, j * 2 + 1] = "────────────";
                }
                template[i * 2 + 4, 15] = "────────────";
                template[i * 2 + 4, 16] = "┤";
                template[i * 2+3, 16] = "│";

                template[i * 2 + 4, 0] = "├";
            }

            for (int i = 0; i < 7; i++)
            {
                template[16, i * 2 + 2] = "┴";
            }

            template[16, 0] = "└";
            template[16, 16] = "┘";
            #endregion
            #region selected
            template[(x-balfelso[0]) * 2 + 2, (y - balfelso[1]) * 2 + 2 ] = "╔";
            template[(x - balfelso[0]) * 2 + 2, (y - balfelso[1]) * 2 + 3] = "════════════";
            template[(x - balfelso[0]) * 2 + 2, (y - balfelso[1]) * 2 + 4] = "╗";
            template[(x - balfelso[0]) * 2 + 3, (y - balfelso[1]) * 2 + 2] = "║";
            template[(x - balfelso[0]) * 2 + 3, (y - balfelso[1]) * 2 + 4] = "║";
            template[(x - balfelso[0]) * 2 + 4, (y - balfelso[1]) * 2 + 2] = "╚";
            template[(x - balfelso[0]) * 2 + 4, (y - balfelso[1]) * 2 + 3] = "════════════";
            template[(x - balfelso[0]) * 2 + 4, (y - balfelso[1]) * 2 + 4] = "╝";

            #endregion
            #endregion
            Console.Clear();
            //Console.WriteLine("template 16:2:" + template[16, 2] + ":<-");
            //Console.WriteLine((balfelso[0]+1).ToString() + " " + (balfelso[1]+1).ToString());
            Console.WriteLine((x).ToString() + " " + (y).ToString() + ": " + table[x - 1][y - 1]);
            Console.WriteLine();
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    Console.Write(template[i, j]);
                }
                Console.Write("\n");
            }
            #region eredeti_template
            /*Console.WriteLine("             ┌────────────┬────────────┬────────────┬────────────┬────────────┬────────────┬────────────┐");
            Console.WriteLine("             │"+stringLimit((balfelso[0]).ToString(),12)+"|" + stringLimit((balfelso[0]+1).ToString(), 12) + "|" + stringLimit((balfelso[0] + 2).ToString(), 12) + "|" + stringLimit((balfelso[0] +3).ToString(), 12) + "|" + stringLimit((balfelso[0] +4).ToString(), 12) + "|" + stringLimit((balfelso[0] +5).ToString(), 12) + "|" + stringLimit((balfelso[0] + 6).ToString(), 12) + "|");
            Console.WriteLine("┌────────────");
            for (int i = balfelso[1]; i < balfelso[1]+7; i++)
            {

            }
            Console.WriteLine("╔════════════╗────────────┬────────────┬────────────┬────────────┬────────────┬────────────┬────────────┐");
            Console.WriteLine("║            ║            │");
            Console.WriteLine("╚════════════╝────────────┤");
            Console.WriteLine("│            │            │");
            Console.WriteLine("└────────────┴────────────┘");*/
            #endregion
            return (x, y, table, balfelso);
        }

        static List<List<string>> addCell(List<List<string>> tablazat, int imax, int jmax, string ertek) {
            List<List<string>> output=new List<List<string>>();
            for (int i = 0; i <= imax; i++)
            {
                output.Add(new List<string>());
                for (int j = 0; j <= jmax; j++)
                {
                    output[i].Add("");
                    try
                    {
                        output[i][j] = tablazat[i][j];
                    }
                    catch (ArgumentOutOfRangeException) {}
                }
            }
            output[imax][jmax] = ertek;
            return output;
        }
        static string stringLimit(string str, int len)
        {
            if (str.Length == len) { return str; }
            else if(str.Length < len)
            {
                while(str.Length < len)
                {
                    str += " ";
                }
                return str;
            }
            else { return str.Substring(0, len - 3) + "..."; }
        }
    }
}
