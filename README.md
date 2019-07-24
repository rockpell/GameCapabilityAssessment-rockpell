# GameCapabilityAssessment-rockpell

여러가지 미니게임을 조준력, 집중력, 사고력, 순발력, 리듬감과 같은 5가지 항목으로 나누어서 결과에 따라 플레이어의 게임능력을 평가하는 게임입니다.

스마일게이트 멤버십(예비 창업자를 위한 지원 프로그램) 10기에 참가한 프로젝트입니다.

이미지와 사운드의 라이센스 문제로 인해 스크립트만을 별도로 올린 저장소입니다.

## 개발환경
Windows10(64bit)

## 작성언어
C#(Unity)

## 플랫폼
모바일(안드로이드)

## 사용 IDE
Unity 2018.1.6f

Visual Studio 2017

## 플레이 스토어
<https://play.google.com/store/apps/details?id=com.mitgames.gca&hl=ko>

## 소개 영상
<https://www.youtube.com/watch?v=uVf8V8zlQik>

## PlayerPrefs 활용 요소
인트로씬 실행 유무 확인시에 사용합니다.
평가 결과를 저장 할 때 사용합니다.
평가 진행 도중 게임을 종료 하였을 때 중단한 시점부터 진행 할 수 있도록 중간 저장 기능에 사용합니다.

## 주요 클래스 설명
### NewGameManager
싱글톤 클래스입니다.

미니게임의 난이도, 결과 등을 관리합니다.

현재 평가중인 항목이 무엇인지, 해당 항목의 결과, 평가의 진행 상태를 SaveMiddleData 함수를 이용하여 저장하여 관리합니다.

평가를 시작하였을 때 ApplyMiddleData 함수를 이용하여 기존에 진행되던 평가가 있다면 현재 평가에 반영하여 진행합니다.

게임을 시작하였을 때 인트로를 진행한 적이 없다면 인트로 씬으로 전환합니다.

### IntroManager
육각이의 게임능력평가에 대한 설명 및 행동을 YukgackAct 클래스를 통해 관리합니다.

인트로는 한번만 실행되도록 NewGameManager의 SaveIntroTrigger 함수를 이용하여 인트로를 진행하였음을 로컬에 저장합니다.

### NewTutorialManager
미니게임을 연습할 수 있는 튜토리얼을 재시작하거나 설명을 다시 보는 등의 기능을 관리합니다.

### CustomSceneManager
Scene 목록 관리 및 전환 함수를 관리합니다.

### ExplanationScreenManager
미니 게임 시작 전 게임 플레이 방법에 대해 설명하는 이미지 및 UI를 관리합니다.

### SBHInputManager

미니게임 Some Body Help Me의 클래스입니다.

플레이어의 입력에 따른 화면 이동 및 총알 발사 버튼을 눌렀을 때 SBHManager의 Shoot 함수를 호출하는 기능을 관리합니다.

### SBHManager
싱글톤 클래스입니다.

미니게임 Some Body Help Me의 클래스입니다.

총알 발사 기능, 적 생성, 시민 생성, 난이도 조절 등을 관리합니다.

### SBHUIManager
미니게임 Some Body Help Me의 클래스입니다.

화면 상에 나타나는 스코어, 남은 총알 개수, 남은 시간, 총구화염 이미지 등 UI를 관리합니다.
