""" 
tcp networking code

server2.py 파일만 실행하면 서버가 열림
""" 


import socket 
import errno
import pickle
import csv
from mr_kim import kim

with open('30min.p', 'rb') as f:
    data = pickle.load(f)

host = '' 
port = 40000 
size = 1024 
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM) 
s.bind((host,port)) 
s.listen() 
print("Server Open")

while True:
    try:
        client, address = s.accept() 
        print ("Client connected.")
        while True:
            # receive = time + ' ' + theta + ' ' + name 
            receive = client.recv(size).decode()
            if len(receive) is 0:
                break
            receive = receive.split()
            print(receive)

            time = receive[0]
            angle = int(float(receive[1]))
            t = int(time) - 1320300
            t = int(t/2)
            msg = ''
            for i in range(0,5):
                line = data[t+i]
                line = ' '.join([str(num) for num in line])
                msg = msg + line + ' '

            if len(receive) is 3:
                name = int(receive[2])
                user_x = 0
                user_y = 0
                kim_x = data[t][data[t].index(name) + 2]
                kim_y = data[t][data[t].index(name) + 3]
                where = kim(user_x, user_y, kim_x, kim_y, angle) 
                print(where)
                msg += where
            send = msg.encode()
            client.send(send)

    except socket.error as e:
        if e.errno != errno.ECONNRESET:
            raise
        pass
    
    
    