import requests
import json

url = 'https://lscluster.hockeytech.com/feed/index.php?feed=modulekit&view=brackets&fmt=json&season_id=67&key=54ad32ee30e379ad&client_code=pjhlon&site_id=2&lang=en&league_id=&callback=angular.callbacks._1'

# get data and format to json
r = requests.get(url)
content = r.text
content = content.split('(', 1)[1]
content = content[:-1]
data = json.loads(content)

# get team data
team_data = []
td = data['SiteKit']['Brackets']['teams']
for _td in td:
    team_data.append(td[_td])
with open('team_data.json', 'w') as f:
    json.dump(team_data, f)

# get matchups
md = data['SiteKit']['Brackets']['rounds']
with open('matchups.json', 'w') as f:
    json.dump(md, f)
