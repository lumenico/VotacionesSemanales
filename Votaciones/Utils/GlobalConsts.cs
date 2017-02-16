using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Votaciones.Utils
{
    public static class GlobalConsts
    {
        public const string C_SEMANA = "IDSEMANA";
        public const string C_CURRENTUSER = "CurrentUser";
        public const string C_SCRIPTCHART = "$(document).ready(function () {{ google.charts.load('current', {{ packages: ['corechart']}});google.charts.setOnLoadCallback(function(){{drawChart(document.getElementById('{0}'), '{1}', '{2}');}});}});";
    }
}