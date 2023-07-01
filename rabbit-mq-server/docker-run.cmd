rem TODO:(dh) Will be change to dockerfile with docker-compose.yml
rem localhost:15672 - 웹 서비스에 접속할 수 있습니다.
rem 기본 아이디 user / 기본 비밀번호 qwer1234
rem 기본 vhost rabbitmq_vhost
rem 서비스 포트 5672
rem 본 서비스는 클러스터 형태로 구성하는걸 가정하지 않았기 때문에 클러스터를 사용하지 않습니다.

docker run ^
--restart="always" ^
--name rabbitmq ^
-e RABBITMQ_DEFAULT_USER=user ^
-e RABBITMQ_DEFAULT_PASS=qwer1234 ^
-e RABBITMQ_DEFAULT_VHOST=rabbitmq_vhost ^
-p 5672:5672 ^
-p 8080:15672 ^
rabbitmq:3-management
