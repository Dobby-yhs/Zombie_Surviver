# Zombie_Surviver
Unity Project with TJ. Zombie Surviver is this game's working title.

### 1/28 기획
#### https://hypnotic-smoke-d5a.notion.site/2023_UNITY-e0e4069efa654f8fb4adfa600fd04731
### 1/29 ~ 2/3 스크립트 완성 및 시행착오
### 2/4 총기 스크립트 완성
#### ~~https://hypnotic-smoke-d5a.notion.site/2023_UNITY_0204-ad4da53bed8b4c128778b510e8a1ba5e~~
### 2/5 추가 총기 스크립트 완성 및 시행착오
#### GunData 상속과 Shooter에서의 GunData 사용법에 대해 생각하기
#### https://hypnotic-smoke-d5a.notion.site/2023_UNITY_0204-8e3234c718804233904b7f015ff5723b
### 2/6 총기 스탯 업그레이드 스크립트 완성 및 시행착오
#### ~~GunData가 할당된 프리팹을 Gun의 변수 값(ex. damage)이 변경될 때 마다 다시 할당해주는 스크립트 작성하기~~
### 2/7 총기 스탯 업그레이드 코드 작성 완료 및 총기 선택 스크립트 완성
#### 프리팹을 재할당해주는 스크립트 없어도 변수 값 변경 가능.. 단순한 실수였다.(GunChoice.cs 스크립트에서 obj를 할당해주지 않아 null 오류가 계속 떴었다.)
### 2/8 총기 선택 스크립트 최적화 및 playerShooter에서 currentGun 할당 방식 수정 중
#### currentGun 대신 Gun 스크립트를 통해 guns 배열을 만들어 오브젝트 선택하는 방식 채택
### 2/9 총기 스크립트 완성 및 좀비 스크립트 작성 중
#### 좀비의 종류 및 데이터를 늘려서 좀비에 적용하기, 좀비 스포너 만들기
### 2/10 좀비 스크립트 대략 완성 및 상인 스크립트 완성
#### 좀비 스크립트 테스트 필요 및 애니메이션 추가 필요, 총기 업그레이드 아이템 스포너 스크립트 작성 필요
### 2/10 아이템 스포너 스크립트 및 총기 업그레이드 관련 스크립트 완성
#### 총기 업그레이드 스탯에 관해서는 토의 통해 수정 예정
### 2/11 상인 스크립트 및 좀비 스포너 스크립트 수정 완료
#### 상인 스크립트 애니메이션은 고정이기에 컴포넌트 안받아오는 것으로 수정, 좀비 스포너 스크립트에는 상인의 활성화 여부에 따라 좀비 정지시키는 코드 삽입
### 2/11 스크립트 맵에 적용, 오류 수정 중
#### [오류 1] "Stop" can only be called on an active agent that has been placed on a NavMesh.UnityEngine.tackTraceUtility:ExtractStackTrace ()Zombie/<UpdatePath>d__25:MoveNext () (at Assets/Scripts/Zombie.cs:141)UnityEngine.onoBehaviour:StartCoroutine (System.Collections.IEnumerator)Zombie:Start () (at Assets/Scripts/Zombie.cs:109)
#### [오류 2] "Resume" can only be called on an active agent that has been placed on a NavMesh.UnityEngine.tackTraceUtility:ExtractStackTrace ()Zombie/<UpdatePath>d__25:MoveNext () (at Assets/Scripts/Zombie.cs:137)UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)
#### [오류 3] "SetDestination" can only be called on an active agent that has been placed on a NavMesh.UnityEngine.AI.avMeshAgent:SetDestination (UnityEngine.Vector3)Zombie/<UpdatePath>d__25:MoveNext () (at Assets/Scripts/Zombie.cs:138)UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)
### 2/12 상인 스폰 및 NavMeshAgent 오류 수정 중
#### 상인 스폰은 성공했지만, 스폰 후 collider가 안됨(MerchantSpawner에 스크립트를 적용하였는데, Merchant 프리팹에 직접적으로 스크립트가 적용이 안된 듯 싶음)
#### 오류는 NavMeshAgent를 좀비 리스트들에 중첩적으로 작성하여 안되는게 아니라 그냥 PauseZombie 함수 안에서 zombies 리스트 하나에서만 해도 오류가 발생했음
### 2/18 상인 스크립트와 상인 스포너 스크립트로 구분 후 적용, 상인 관련 문제 해결 완료
#### 좀비 4마리 적용, 아이템 스폰 적용, 총기 애니메이션 적용, 상인 접촉 후 좀비도 멈추게 적용(NavMeshAgent 오류) 필요
### 2/18 time 일시정지 수정 중
#### 플레이어만 일시정지 안되게 수정 필요
### 2/19 time 일시정지 수정 중
#### 플레이어만 timeScale 1로 설정했는데, ZombieSpawner.cs에서 merchantIsCollider 값이 Merchant.cs의 isCollider 값을 받아오지못함. 수정 필요
### 2/21 time 일시정지 수정 및 좀비 스폰 방식 변경 완료
#### waveTime UI 수정 필요
### 2/23 LightZombie 추가 완료
#### LightZombie 애니메이션 처리 확인 및 time 일시 정지 확인 필요
### 2/25 time 일시정지 수정 완료 및 모든 좀비 스크립트 추가 완료
#### 스크립트 각 좀비에 적용 필요
### 2/28 상인 UI 추가 완료 및 모든 좀비 적용 완료
#### 스탯 변경과 변경된 스탯 적용, 밸런스 조정 필요
### 3/1 좀비 스포너 스크립트 수정 중
#### get set 방식으로 zombieKill에 따른 시간 변화를 인지하게 하려 했지만 실패
### 3/2 좀비 스포너 스크립트 수정 완료
#### 시간 초 설정만 수정하면 됨
### 3/3 상인 UI 적용 수정 완료 및 밸런스 조정
### 3/3 상인 충돌 문제 수정 완료 및 밸런스 조정, 상인 UI 오류 발견 및 수정 중
#### GameManager.cs의 coin이 초기화가 안되는 오류
### 3/6 상인 UI 오류 수정 완료
### 3/7 HealthPack, Coin 밸런스 조정 완료
### 3/11 밸런스 조정 완료