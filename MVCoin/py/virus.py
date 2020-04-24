#!/usr/bin/env python3
# -*- coding: utf-8 -*

import requests

# Summery report.
report = ""

# Taiwan overview from CDC home page.
overview_request = requests.get(
    'https://covid19dashboard.cdc.gov.tw/dash3',
    headers={'Origin': 'https://594250627-atari-embeds.googleusercontent.com'}
)
overview_data = overview_request.json()["0"]

# Taiwan confirm cases detail from CDC open data portal.
# Daily update. Week is statistical interval, not update frequency.
detail_request = requests.get('https://od.cdc.gov.tw/eic/Weekly_Age_County_Gender_19CoV.json')
detail_data = detail_request.json()

# Global overview from CDC home page.
global_request = requests.get(
    'https://covid19dashboard.cdc.gov.tw/dash2',
    headers={'Origin': 'https://594250627-atari-embeds.googleusercontent.com'}
)
global_data = global_request.json()["0"]


report += "目前全球共%s例確診，死亡%s例，致死率%s\n" % (
    global_data["cases"],
    global_data["deaths"],
    global_data["cfr"]
)

report += "目前台灣地區共%d例確診，新增%d例，死亡%d例\n" % (
    overview_data["確診"],
    overview_data["昨日確診"],
    overview_data["死亡"]
)

# Important districts in Taiwan
districts = ["台北市", "新竹縣", "高雄市"]

# Analyse case detail.
for district in districts:
    confirm = 0
    abroad = 0
    for entry in detail_data:
        if entry["縣市"] == district:
            confirm += 1
            if entry["是否為境外移入"] == "是":
                abroad += 1

    report += "%s區域確診數累計%d例，其中%d例為境外移入\n" % (district, confirm, abroad)

print(report)



