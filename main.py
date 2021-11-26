from mlagents_envs.environment import UnityEnvironment
from gym_unity.envs import UnityToGymWrapper
from gym.wrappers import TimeLimit

from stable_baselines3 import DQN

verbose = 2

config = {
    'policy_type': 'MlpPolicy',
    'total_timesteps': 10
}

env_name = None # None if in editor, else path to .exe
unity_env = UnityEnvironment(
    file_name=env_name, seed=1, side_channels=[])
env = UnityToGymWrapper(unity_env, allow_multiple_obs=False)
env = TimeLimit(env, max_episode_steps=1000)

agent = DQN('MlpPolicy', env, verbose=verbose)

agent.learn(config['total_timesteps'])

agent.save('./agents/dqn_unity')

env.close()

env_name = None # None if in editor, else path to .exe
unity_env = UnityEnvironment(
    file_name=env_name, seed=1, side_channels=[])
env = UnityToGymWrapper(unity_env, allow_multiple_obs=False)
env = TimeLimit(env, max_episode_steps=1000)

agent = DQN.load('./agents/dqn_unity', env, verbose=verbose)

# Test loop
episodes = 10
for ep in range(episodes):
    done = False
    obs = env.reset()
    while not done:
        action, states = agent.predict(obs, deterministic=True)
        obs, reward, done, info = env.step(action)
        