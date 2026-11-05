# 일본어 단어장 (VocaProgram)

C# WPF로 만든 일본어 단어 학습 프로그램입니다. 단어 데이터는 Firebase Realtime Database에서 불러옵니다.

## 주요 기능

- 단어 보기: 랜덤 / 순서대로, "둘 다 보기 · 단어만 보기 · 뜻만 보기" 전환
- 히라가나표: あ·い·う·え·お단별 보기
- 단어 Quiz: 뜻을 보고 단어 맞히기
- 의미 Quiz: 단어를 보고 뜻 맞히기

## 기술 스택

- C#, WPF (.NET Framework)
- Firebase Realtime Database ([FireSharp](https://github.com/ziyasal/FireSharp) 2.0.4)

## 구조

```
pro1.sln                 # Visual Studio 솔루션
pro1/MainWindow.xaml     # 메인 단어장 화면
pro1/HiraganaWindow.xaml # 히라가나표
pro1/WordQuiz.xaml       # 단어 Quiz
pro1/QuizWindow.xaml     # 의미 Quiz
packages/                # NuGet 패키지
```

## 실행 방법

Firebase 접속 정보는 코드에 넣지 않고 환경 변수에서 읽습니다. 실행 전에 Windows 사용자 환경 변수를 설정합니다.

```bash
setx FIREBASE_BASE_PATH "https://<프로젝트 ID>-default-rtdb.firebaseio.com/"
setx FIREBASE_AUTH_SECRET "<데이터베이스 비밀번호>"
```

환경 변수를 설정한 뒤 Visual Studio를 다시 열고 `pro1.sln`을 실행합니다.
