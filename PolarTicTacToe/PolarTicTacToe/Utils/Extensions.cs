using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolarTicTacToe.Utils
{
    public static class Extentions
    {

        public static List<TSource> ToList<TSource, TSink>(this IEnumerable<TSink> enumerator, Func<TSink, TSource> action)
        {
            List<TSource> list = new List<TSource>();

            foreach (var value in enumerator)
            {
                list.Add(action(value));
            }

            return list;
        }


        public static List<SelectListItem> ToSelectList<TSink>(this IEnumerable<TSink> enumerator, Func<TSink, string> keySelector, Func<TSink, string> valueSelector)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();


            foreach (var value in enumerator)
            {
                SelectListItem item = new SelectListItem();
                item.Text = valueSelector(value);
                item.Value = keySelector(value);
                selectList.Add(item);
            }

            return selectList;
        }
        public static List<SelectListItem> ToSelectList<TSink>(this IEnumerable<TSink> enumerator, Func<TSink, string> keySelector, Func<TSink, string> valueSelector, string selectedValue)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();


            foreach (var value in enumerator)
            {
                SelectListItem item = new SelectListItem();
                item.Text = valueSelector(value);
                item.Value = keySelector(value);
                item.Selected = (item.Value == selectedValue);
                selectList.Add(item);
            }

            return selectList;
        }
    }
}